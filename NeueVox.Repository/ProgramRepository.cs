using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IProgramRepository : IBaseRepository<Program>
{

}

public class ProgramRepository : BaseRepository<Program>,IProgramRepository
{
  public ProgramRepository(NeueVoxContext context):base(context){}

}
