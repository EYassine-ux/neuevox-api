using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class ScheduleResponseDTO
{
  public string ClassName { get; set; }
  public Guid ClassId { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public WeekDays DayOfWeek { get; set; }

  public string RoomNumber { get; set; }
}
