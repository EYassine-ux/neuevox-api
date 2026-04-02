using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IStudentClassRepository : IBaseRepository<StudentClass>
{

}

public class StudentClassRepository : BaseRepository<StudentClass>,IStudentClassRepository
{

  public StudentClassRepository(NeueVoxContext context):base(context){}

}
