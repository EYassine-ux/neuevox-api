namespace NeueVox.Model.DTOs.ReponseDTO;

  public class StudentGradesResponseDTO
  {
    public string ClassName { get; set; }
    public string EvaluationTitle { get; set; }
    public decimal EvaluationWeight { get; set; }
    public decimal EvaluationGrade { get; set; }
  }
