namespace NeueVox.Model.NeuevoxModel;

public class Document
{
  public Guid DocumentId { get; set; }
  public Guid ClassId { get; set; }
  public Class? Class { get; set; }
  public required string FileName { get; set; }
  public required string FileUrl { get; set; }
  public required string FileType { get; set; }

}
