namespace NeueVox.Model.DTOs.ReponseDTO;

public class StudentResponseDTO
{
  public Guid StudentId { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string SchoolEmail { get; set; }
  public string? ProfilePictureUrl { get; set; }
  public string? PhoneNumber {get; set;}
  public string? Coordination {get; set;}
  public string? ProgramName { get; set; }
  public DateTime AdmissionDate  { get; set; }
}
