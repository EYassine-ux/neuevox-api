using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository.MongoDbRepository;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
  Task<IEnumerable<Schedule>> GetScheduleForStudent(Guid studentId);
  Task<IEnumerable<Schedule>> GetScheduleForProfessor(Guid professorId);
}


public class ScheduleRepository : BaseRepository<Schedule>,IScheduleRepository
{
  public ScheduleRepository(NeueVoxContext dbContext) : base(dbContext)
  {

  }

  public async Task<IEnumerable<Schedule>> GetScheduleForStudent(Guid studentId)
  {
    var schedule = await DbContext.Schedules.AsNoTracking()
      .Where(s => s.Class != null && s.Class.StudentClasses.Any(sc => sc.StudentId == studentId))
      .Include(s => s.Class)
      .ThenInclude(c => c.Course)
      .ToListAsync();

    return schedule;
  }

  public async Task<IEnumerable<Schedule>> GetScheduleForProfessor(Guid professorId)
  {
    var schedule = await DbContext.Schedules.AsNoTracking()
      .Where(s => s.Class != null && s.Class.ProfessorId == professorId)
      .Include(s => s.Class)
      .ThenInclude(c => c.Course)
      .ToListAsync();

    return schedule;
  }
}
