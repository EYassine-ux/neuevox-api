using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IGradeRepository : IBaseRepository<Grade>
{
}

public class GradeRepository : BaseRepository<Grade>,IGradeRepository
{
  public GradeRepository(NeueVoxContext dbContext) : base(dbContext)
  {
  }

}
