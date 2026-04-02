namespace NeueVox.Model.DTOs.ReponseDTO;

public class StudentSubmissionResponse
{
  public Guid Id { get; set; }
  public Guid EvaluationId { get; set; }
  public Guid StudentId { get; set; }
  public DateTime SubmissionDate { get; set; }
  public required string FileUrl { get; set; }
}
