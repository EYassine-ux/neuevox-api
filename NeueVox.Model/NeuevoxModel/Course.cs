namespace NeueVox.Model.NeuevoxModel;

public class Course
{
    public Guid CourseId { get; set; }
    public required string CourseCode {get; set;}
    public required string CourseTitle {get; set;}

    public ICollection<Class> Classes {get; set;} = new List<Class>();
}
