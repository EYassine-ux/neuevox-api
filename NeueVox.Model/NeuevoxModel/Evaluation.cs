using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.NeuevoxModel;

public class Evaluation
{
    public Guid EvaluationId { get; set; }

    public Guid ClassId { get; set; }
    public Class? Class { get; set; }

    public required string EvaluationTitle  { get; set; }
    public EvaluationType EvaluationType { get; set; }

    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Weight { get; set; }
    public decimal MaxScore { get; set; }
    public DateTime DueDate { get; set; }


    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<StudentSubmission> StudentSubmissions { get; set; } = new List<StudentSubmission>();
}
