using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class AddEvaluationDTO
{

  public Guid ClassId { get; set; }
  public string EvaluationTitle  { get; set; }
  public EvaluationType EvaluationType { get; set; }

  public decimal Weight { get; set; }
  public decimal MaxScore { get; set; }
  public DateTime DueDate { get; set; }
}
