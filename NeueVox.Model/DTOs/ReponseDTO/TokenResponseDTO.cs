using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class TokenResponseDTO
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public UserRole userRole { get; set; }
  public required string UserId { get; set; }
  public required string AccessToken { get; set; }
  public required string RefreshToken { get; set; }
}
