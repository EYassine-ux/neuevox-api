namespace NeueVox.Model.DTOs.ReponseDTO;

public class ProfessorResponseDTO
{
  public Guid ProfessorId { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string SchoolEmail { get; set; } = string.Empty;
  public string? Biography{get; set;}
  public string? ProfilePictureUrl { get; set; }
  public string? coordination {get; set;}

  public string? PhoneNumber {get; set;}
  public string OfficeNumber { get; set; } = string.Empty;
  public string Department { get; set; } = string.Empty;
}
