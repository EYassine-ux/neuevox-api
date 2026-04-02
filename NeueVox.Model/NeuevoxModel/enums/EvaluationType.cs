using System.Text.Json.Serialization;

namespace NeueVox.Model.NeuevoxModel.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EvaluationType
{
  Examen,
  TravailPratique,
  Devoirs
}
