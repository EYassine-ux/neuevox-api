using System.Text.Json.Serialization;

namespace NeueVox.Model.NeuevoxModel.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WeekDays
{
  MONDAY,
  TUESDAY,
  WEDNESDAY,
  THURSDAY,
  FRIDAY,
  SATURDAY,
  SUNDAY
}
