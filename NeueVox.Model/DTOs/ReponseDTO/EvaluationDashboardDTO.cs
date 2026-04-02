using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class EvaluationDashboardDTO
{

  public Guid EvaluationId { get; set; }
  public required string Title { get; set; }
  public required string CourseName { get; set; }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public EvaluationType EvaluationType { get; set; }

  public decimal Weight { get; set; }
  public DateTime DueDate { get; set; }

}
