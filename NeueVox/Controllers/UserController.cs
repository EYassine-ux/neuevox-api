using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.enums;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;
  public  UserController(IUserService service)
  {
    _userService = service;
  }

  [HttpGet("{userId:guid}", Name = "GetUserById")]
  public async Task<ActionResult<UserResponseDTO?>> GetUserById([FromRoute] Guid userId)
  {
    var user = await _userService.GetUserById(userId);
    return Ok(user);
  }

  [HttpGet(Name = "GetUsers")]
  public async Task<ActionResult<List<UserResponseDTO>>> GetUsers()
  {
    var users = await  _userService.GetAllUsers();
    return Ok(users);
  }

  [HttpGet("email/{email}", Name = "GetUserByEmail")]
  public async Task<ActionResult<UserResponseDTO>> GetUserByEmail([FromRoute] string email)
  {
    var user = await _userService.GetUserByEmail(email);
    return Ok(user);
  }

  [HttpDelete("{userId:guid}")]
  public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
  {
    var user = await _userService.DeleteAsync(userId);
    if(!user) return BadRequest();
    return Ok(user);
  }

  [HttpPost]
  public async Task<IActionResult> AddUser([FromBody] AddUserDTO userDto)
  {

    var user = await _userService.AddUserAsync(userDto);
    if (user == null) return BadRequest(userDto);
    return Ok(user);
  }

  [HttpPut("{userId:guid}")]
  public async Task<IActionResult> UpdateUser([FromBody] AddUserDTO userDto,Guid userId )
  {

    var updatedUser = await _userService.UpdateUserAsync(userDto,userId);
    if (updatedUser == null) return BadRequest(userDto);
    return Ok(updatedUser);

  }

}

