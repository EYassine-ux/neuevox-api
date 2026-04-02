using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IAuthService
{
  Task<User?> RegisterAsync(AddUserDTO request);
  Task<TokenResponseDTO?> Login(LoginUserDTO request);
  Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request);
}

public class AuthService(IUserRepository _repo,IConfiguration configuration) : IAuthService
{
    public async Task<TokenResponseDTO?> Login(LoginUserDTO request)
    {
        var user = await _repo.GetUserByEmail(request.Email);
        if (!(await _repo.VerifyUserByEmail(request.Email)))
        {
            return null;
        }
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
            == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    private async Task<TokenResponseDTO> CreateTokenResponse(User user)
    {
        return new TokenResponseDTO
        {
            userRole = user.Role,
            UserId = user.UserId.ToString(),
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO request)
    {
        var user = await _repo.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if(user is null)
        {
            return null;
        }
        return await CreateTokenResponse(user);
    }

    private async Task<String> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refrechToken = GenerateRefreshToken();
        user.RefreshToken = refrechToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _repo.UpdateAsync(user,user.UserId);
        return refrechToken;
    }

    private string? CreateToken(User user)
    {
        var claims = new List<Claim>
         {
             new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
             new Claim(ClaimTypes.Email,user.SchoolEmail),
             new Claim(ClaimTypes.Role,user.Role.ToString())
         };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task<User?> RegisterAsync(AddUserDTO request)
    {
        if(await _repo.VerifyUserByEmail(request.SchoolEmail))
            {
            return null;
            }

        var user = new User {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = request.Password,
            SchoolEmail = request.SchoolEmail,
        };
        var hashedPassword = new PasswordHasher<User>()
             .HashPassword(user, request.Password);

        user.PasswordHash = hashedPassword;

        var createdUser = await _repo.CreateAsync(user);

        return createdUser;
    }


}
