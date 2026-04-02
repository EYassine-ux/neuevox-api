using System.Text.Json.Serialization;
using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs.ReponseDTO;

public class RefreshTokenRequestDTO
{
  public Guid UserId { get; set; }

  public required string RefreshToken { get; set; }
}
