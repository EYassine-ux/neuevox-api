using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IUserService : IBaseService<User>
{
  Task<UserResponseDTO?> GetUserByEmail(string email);
  Task<UserResponseDTO?> GetUserById(Guid id);
  Task<IEnumerable<UserResponseDTO>> GetAllUsers();
  Task<User?> AddUserAsync(AddUserDTO userDto);
  Task<User?> UpdateUserAsync(AddUserDTO userDto, Guid id);
}

public class UserService : BaseService<User>, IUserService
{
  private readonly IUserRepository _userRepository;
  public UserService(IUserRepository repository) : base(repository)
  {
    _userRepository = repository;
  }

  public async Task<UserResponseDTO?> GetUserByEmail(string email)
  {
    var rawUser = await _userRepository.GetUserByEmail(email);
    if (rawUser is null) return null;

    var user = MapToResponseDTO(rawUser);

    return user;
  }

  public async Task<UserResponseDTO?> GetUserById(Guid id)
  {
    var rawUser = await _userRepository.GetByIdAsync(id);
    if (rawUser is null) return null;
    var user = MapToResponseDTO(rawUser);

    return user;
  }

  public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
  {
    var rawUsers = await _userRepository.GetAllAsync();

    var users = rawUsers.Select(MapToResponseDTO).ToList();

    return users;

  }

  public async Task<User?> AddUserAsync(AddUserDTO userDto)
  {
    var addedUser = new User
    {
      FirstName = userDto.FirstName,
      LastName = userDto.LastName,
      PasswordHash = userDto.Password,
      Role = userDto.Role,
      ProfilePictureUrl = userDto.ProfilePictureUrl,
      Coordination = userDto.Coordination,
      PhoneNumber = userDto.PhoneNumber,
      SchoolEmail = userDto.SchoolEmail,
      CreatedAt = DateTime.UtcNow
    };
    var hashedPassword = new PasswordHasher<User>()
      .HashPassword(addedUser, userDto.Password);
    addedUser.PasswordHash = hashedPassword;
    return await _userRepository.CreateAsync(addedUser);
  }

  public async Task<User?> UpdateUserAsync(AddUserDTO userDto, Guid id)
  {
    var oldUser = await _userRepository.GetByIdAsync(id);
    if (oldUser is null) return null;
    oldUser.FirstName = userDto.FirstName;
    oldUser.LastName = userDto.LastName;
    oldUser.PasswordHash = userDto.Password;
    oldUser.Role = userDto.Role;
    oldUser.SchoolEmail = userDto.SchoolEmail;
    oldUser.ProfilePictureUrl = userDto.ProfilePictureUrl;
    oldUser.PhoneNumber = userDto.PhoneNumber;
    oldUser.Coordination = userDto.Coordination;

    if (!string.IsNullOrWhiteSpace(userDto.Password))
    {
      oldUser.PasswordHash = new PasswordHasher<User>()
        .HashPassword(oldUser, userDto.Password);
    }

    return await _userRepository.UpdateAsync(oldUser, id);
  }


  private UserResponseDTO MapToResponseDTO(User user)
  {
    return new UserResponseDTO
    {
      UserId =  user.UserId,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Role = user.Role,
      ProfilePictureUrl = user.ProfilePictureUrl,
      Coordination = user.Coordination,
      PhoneNumber = user.PhoneNumber,
      SchoolEmail = user.SchoolEmail,
      CreatedAt = user.CreatedAt
    };
  }

}
