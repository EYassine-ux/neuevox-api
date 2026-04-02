using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
  [Authorize(Roles = "Admin")]
  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(AddUserDTO request)
  {
    var user = await authService.RegisterAsync(request);
    if(user == null)
    {
      return BadRequest("Email already exist");
    }
    return Ok(user);
  }

  [HttpPost("Login")]
  public async Task<ActionResult<TokenResponseDTO>> Login(LoginUserDTO request)
  {
    var response = await authService.Login(request);

    if(response == null)
    {
      return BadRequest("Email or Password incorrect");
    }
    return Ok(response);
  }



  [HttpPost("refresh-token")]
  public async Task<ActionResult<TokenResponseDTO>> RefreshToken(RefreshTokenRequestDTO request)
  {
    var result = await authService.RefreshTokensAsync(request);
    if(result is null || result.AccessToken is null || result.RefreshToken is null)
    {
      return Unauthorized("Invalid refresh Token.");
    }
    return Ok(result);
  }

}
