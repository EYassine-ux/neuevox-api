using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class AddScheduleDTO
{
  public Guid ClassId { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
  public WeekDays DayOfWeek { get; set; }
  public string RoomNumber { get; set; }
}
