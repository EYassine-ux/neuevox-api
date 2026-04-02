namespace NeueVox.Model.NeuevoxModel;

public class Grade
{
    public Guid GradeId { get; set; }

    // Links this specific Grade to the one Evaluation it belongs to.
    public Guid EvaluationId { get; set; }
    public Evaluation? Evaluation { get; set; }

    //a student can have multiple grades
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public decimal Score { get; set; }
}
