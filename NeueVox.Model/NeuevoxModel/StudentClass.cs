using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.NeuevoxModel;

public class StudentClass
{
    public Guid StudentClassId {get; set;}

    //the one to many relationship between the studentClass and the Class
    public Guid ClassId { get; set; }
    public Class? Class { get; set; }

    //the one to many relationship between the studentClass and the Student
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }

    public decimal? FinalGrade {get; set;}

    public ClassStatus ClassStatus {get; set;}
}
