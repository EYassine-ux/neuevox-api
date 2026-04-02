using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface ICourseRepository : IBaseRepository<Course>
{
}

public class CourseRepository : BaseRepository<Course>,ICourseRepository
{
  public CourseRepository(NeueVoxContext  context):base(context){}


}
