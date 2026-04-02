namespace NeueVox.Model.DTOs;

public class AddStudentSubmission
{
  public Guid EvaluationId { get; set; }
  public Guid StudentId { get; set; }
  public required string FileUrl { get; set; }
}
