namespace NeueVox.Model.NeuevoxModel;

public class Professor : User
{

    public string? OfficeNumber{get; set;}

    public required string Department{get; set;}
    public string? Biography{get; set;}
    public DateTime HireDate{get; set;}

    //one to many relationship between the professor and the classes the professor can have multiple classes but the class must be only for one professor (for simplicity)
    public ICollection<Class> Classes {get; set;} = new List<Class>();



}
