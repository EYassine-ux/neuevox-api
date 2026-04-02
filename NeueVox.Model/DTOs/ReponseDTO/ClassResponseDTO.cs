namespace NeueVox.Model.DTOs.ReponseDTO;

public class ClassResponseDTO
{
  public Guid ClassId { get; set; }
  public required string CourseTitle { get; set; }
  public required string ClassNumber { get; set; }
  public required string Semester { get; set; }
  public required string ProfessorName { get; set; }
  public required string ProfessorOffice { get; set; }
}
