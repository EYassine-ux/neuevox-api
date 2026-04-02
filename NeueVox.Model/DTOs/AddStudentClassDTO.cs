using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class AddStudentClassDTO
{

  public Guid ClassId { get; set; }
  public Guid StudentId { get; set; }
  public decimal FinalGrade {get; set;}
  public ClassStatus ClassStatus {get; set;}

}
