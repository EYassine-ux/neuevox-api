namespace NeueVox.Model.NeuevoxModel;

public class Student : User
{
    public Guid? ProgramId {get; set;}
    public Program? Program {get; set;}
    public DateTime AdmissionDate {get; set;}
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    public ICollection<StudentSubmission> StudentSubmissions { get; set; } = new List<StudentSubmission>();
}
