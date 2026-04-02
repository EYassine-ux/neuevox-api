namespace NeueVox.Model.DTOs.ReponseDTO;

public class DocumentResponseDTO
{

  public Guid DocumentId { get; set; }
  public Guid ClassId { get; set; }
  public required string FileName { get; set; }
  public required string FileUrl { get; set; }
  public required string FileType { get; set; }
}
