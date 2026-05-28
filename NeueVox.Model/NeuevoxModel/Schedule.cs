using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.NeuevoxModel;

public class Schedule
{
  public Guid ScheduleId { get; set; }
  public Guid ClassId { get; set; }
  public Class? Class { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
  public WeekDays DayOfWeek { get; set; }

  public string RoomNumber { get; set; }



}
