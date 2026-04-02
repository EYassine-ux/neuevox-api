using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IProfessorRepository : IBaseRepository<Professor>
{
  Task<IEnumerable<Professor>> GetByDepartement(string depart);
}

public class ProfessorRepository : BaseRepository<Professor> , IProfessorRepository
{
    public  ProfessorRepository(NeueVoxContext dbContext) : base(dbContext)
    {}

    public async Task<IEnumerable<Professor>> GetByDepartement(string depart)
    {
        return await DbSet.AsNoTracking().Where(p=> p.Department == depart).ToListAsync();
    }
}
