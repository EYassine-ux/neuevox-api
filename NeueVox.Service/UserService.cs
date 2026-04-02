using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IUserService : IBaseService<User>
{
  Task<User?> GetUserByEmail(string email);
  Task<User?> AddUserAsync(AddUserDTO userDto);
  Task<User?> UpdateUserAsync(AddUserDTO userDto,Guid id);

}

public class UserService : BaseService<User>,IUserService
{
  private readonly IUserRepository _userRepository;
  public UserService(IUserRepository repository) : base(repository)
  {
    _userRepository = repository;
  }

  public async Task<User?> GetUserByEmail(string email)
  {
    return await _userRepository.GetUserByEmail(email);
  }

  public async Task<User?> AddUserAsync(AddUserDTO userDto)
  {
    var addedUser = new User
    {
      FirstName = userDto.FirstName,
      LastName = userDto.LastName,
      PasswordHash = userDto.Password,
      Role = userDto.Role,
      SchoolEmail = userDto.SchoolEmail,
      CreatedAt =  DateTime.UtcNow
    };
    var hashedPassword = new PasswordHasher<User>()
      .HashPassword(addedUser, userDto.Password);
    addedUser.PasswordHash = hashedPassword;
    return await _userRepository.CreateAsync(addedUser);
  }

  public async Task<User?> UpdateUserAsync(AddUserDTO userDto,Guid id)
  {
    var oldUser = await _userRepository.GetByIdAsync(id);
    if(oldUser is null ) return null;
    oldUser.FirstName = userDto.FirstName;
    oldUser.LastName = userDto.LastName;
    oldUser.PasswordHash = userDto.Password;
    oldUser.Role = userDto.Role;
    oldUser.SchoolEmail = userDto.SchoolEmail;

    if (!string.IsNullOrWhiteSpace(userDto.Password))
    {
      oldUser.PasswordHash = new PasswordHasher<User>()
        .HashPassword(oldUser, userDto.Password);
    }

    return await _userRepository.UpdateAsync(oldUser,id);
  }

}
