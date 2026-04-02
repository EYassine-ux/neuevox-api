using System.Text.Json.Serialization;

namespace NeueVox.Model.NeuevoxModel.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClassStatus
{
    NOT_STARTED,
    STARTED,
    FINISHED
}
