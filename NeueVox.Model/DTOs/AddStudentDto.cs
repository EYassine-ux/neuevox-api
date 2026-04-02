namespace NeueVox.Model.DTOs;

public class AddStudentDto
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string SchoolEmail { get; set; }
  public string? ProfilePictureUrl { get; set; }
  public string? PhoneNumber {get; set;}

  public string? Coordination {get; set;}
  public string Password { get; set; }
  public Guid ProgramId { get; set; }
}
