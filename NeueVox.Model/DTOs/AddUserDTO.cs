using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class AddUserDTO
{

  public required string FirstName {get; set;}
  public required string LastName {get; set;}
  public required string SchoolEmail {get; set;}
  public required string Password {get; set;}

  public string? ProfilePictureUrl { get; set; }
  public string? PhoneNumber {get; set;}

  public string? Coordination {get; set;}

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public UserRole Role {get; set;}

}
