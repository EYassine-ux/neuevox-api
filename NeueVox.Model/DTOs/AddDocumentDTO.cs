namespace NeueVox.Model.DTOs;

public class AddDocumentDTO
{
  public Guid ClassId { get; set; }
  public required string FileName { get; set; }
  public required string FileUrl { get; set; }
  public required string FileType { get; set; }
}
