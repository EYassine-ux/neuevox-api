namespace NeueVox.Model.DTOs;

public class AddGradeDTO
{
  public Guid EvaluationId { get; set; }
  public Guid StudentId { get; set; }
  public decimal Score { get; set; }
}
