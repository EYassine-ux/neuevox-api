using System.Text.Json.Serialization;

namespace NeueVox.Model.NeuevoxModel.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    ADMIN,
    GUEST,
    PROFESSOR,
    STUDENT
}
