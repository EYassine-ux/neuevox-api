namespace NeueVox.Model.NeuevoxModel;

public class Class
{
    public Guid ClassId {get; set;}

    //adding the professorId and the professor so they are linked this is the one side
    public Guid ProfessorId {get; set;}
    public Professor? Professor {get; set;}

    //adding the professorId and the professor so they are linked this is the one side
    public Guid CourseId {get; set;}
    public Course? Course {get; set;}

    public string? ClassNumber {get; set;}

    public required string Semester {get; set;}

    //adding both the one to many relationship between the class and the evaluation and studentClass
    public ICollection<Evaluation> Evaluations {get; set;} = new List<Evaluation>();
    public ICollection<StudentClass> StudentClasses {get; set;} = new List<StudentClass>();
    public ICollection<Document> Documents {get; set;} = new List<Document>();
}
