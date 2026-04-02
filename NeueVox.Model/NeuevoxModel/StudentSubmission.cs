using System.ComponentModel.DataAnnotations;

namespace NeueVox.Model.NeuevoxModel;

public class StudentSubmission
{
  [Key]
  public Guid SubmissionId { get; set; }
  public Guid EvaluationId { get; set; }
  public Evaluation? Evaluation { get; set; }
  public Guid StudentId { get; set; }
  public Student? Student { get; set; }
  public DateTime SubmissionDate { get; set; }
  public required string FileUrl { get; set; }
}
