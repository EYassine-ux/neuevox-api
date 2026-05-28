using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class UserResponseDTO
{
  public Guid UserId { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string SchoolEmail { get; set; }
  public UserRole Role { get; set; }
  public string? ProfilePictureUrl { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Coordination { get; set; }
  public DateTime CreatedAt { get; set; }
}
