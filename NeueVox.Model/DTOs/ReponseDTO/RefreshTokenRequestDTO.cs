namespace NeueVox.Model.DTOs.ReponseDTO;

public class RefreshTokenRequestDTO
{
  public Guid UserId { get; set; }

  public required string RefreshToken { get; set; }
}
