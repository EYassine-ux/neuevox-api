using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.DTOs;

public class ReportCardDTO
{
  public string CourseTitle { get; set; }
  public string ProfessorName { get; set; }
  public decimal FinalGrade { get; set; }
  public ClassStatus ClassStatus { get; set; }
}
