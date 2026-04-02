using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IUserRepository : IBaseRepository<User>
{

    Task<User?> GetUserByEmail(string email);
    public Task<bool> VerifyUserByEmail(string email);

    Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);

}

public class UserRepository : BaseRepository<User>,IUserRepository
{

    public UserRepository(NeueVoxContext dbContext) :  base(dbContext)
    {
    }


    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await DbSet.FirstOrDefaultAsync(u=>u.SchoolEmail == email);
        if (user == null)
        {
        }
        return user;
    }

    public Task<bool> VerifyUserByEmail(string email)
    {
      var findUserByEmail = DbSet.AnyAsync(x => x.SchoolEmail == email);

      return findUserByEmail;
    }

    public async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
      var user = await DbSet.FindAsync(userId);
      if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
      {
        return null;
      }
      return user;
    }



}
