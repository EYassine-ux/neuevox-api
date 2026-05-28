using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class ClassResponseDTO
{
  public Guid ClassId { get; set; }
  public required string CourseTitle { get; set; }
  public required string ClassNumber { get; set; }
  public required string Semester { get; set; }
  public required string ProfessorName { get; set; }
  public required string ProfessorOffice { get; set; }
  public List<ScheduleDetailDTO> Schedules { get; set; } = new();
}

public class ScheduleDetailDTO
{
  public required string TimeRange { get; set; }
  public string? Room { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public WeekDays DayOfWeek { get; set; }
}




