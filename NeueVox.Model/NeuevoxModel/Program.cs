namespace NeueVox.Model.NeuevoxModel;

public class Program
{
    public Guid ProgramId { get; set; }
    public required string ProgramName {get; set;}
    public int ProgramCode {get; set;}


    public ICollection<Student> Students {get; set;} = new List<Student>();
}
