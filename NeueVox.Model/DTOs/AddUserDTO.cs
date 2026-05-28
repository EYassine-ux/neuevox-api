using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class AddUserDTO
{

  [JsonPropertyName("firstName")]
  public required string FirstName { get; set; }
  [JsonPropertyName("lastName")]
  public required string LastName { get; set; }

  [JsonPropertyName("schoolEmail")]
  public required string SchoolEmail { get; set; }

  [JsonPropertyName("passwordHash")]
  public required string Password { get; set; }

  public string? ProfilePictureUrl { get; set; }
  public string? PhoneNumber { get; set; }

  public string? Coordination { get; set; }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  [JsonPropertyName("role")]
  public UserRole Role { get; set; } = UserRole.STUDENT;
}
