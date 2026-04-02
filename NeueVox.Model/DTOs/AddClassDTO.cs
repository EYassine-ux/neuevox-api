namespace NeueVox.Model.DTOs;

public class AddClassDTO
{

  public Guid ProfessorId {get; set;}

  public Guid CourseId {get; set;}

  public string ClassNumber {get; set;}

  public string Semester {get; set;}
}
