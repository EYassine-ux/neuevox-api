namespace NeueVox.Model.DTOs.ReponseDTO;

public class EvaluationDetailResponseDTO
{
  public Guid EvaluationId { get; set; }
  public string Title { get; set; }
  public string CourseName { get; set; }
  public string EvaluationType { get; set; }
  public decimal Weight { get; set; }
  public DateTime DueDate { get; set; }
  public string? Description { get; set; }

  public string? StartTime { get; set; }
  public string? EndTime { get; set; }
}
