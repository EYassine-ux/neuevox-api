namespace NeueVox.Model.DTOs.ReponseDTO;

public class GradesClassResponseDTO
{
    public Guid EvauationId { get; set; }
    public required string EvaluationTitle { get; set; }
    public decimal? EvaluationGrade { get; set; }
    public decimal EvaluationWeight { get; set; }
    public required string EvaluationType { get; set; }
    public DateTime DueDate { get; set; }
    public decimal EvaluationAverage { get; set; }


}
