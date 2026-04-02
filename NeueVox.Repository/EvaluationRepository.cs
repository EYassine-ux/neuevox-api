using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IEvaluationRepository : IBaseRepository<Evaluation>
{
  Task<IEnumerable<Evaluation>> GetClassEvaluationsForStudentAsync(Guid classId,Guid studentId);
  Task<IEnumerable<Evaluation>> GetAllEvaluationsForStudentAsync(Guid studentId);
}

public class EvaluationRepository : BaseRepository<Evaluation>,IEvaluationRepository
{
  public EvaluationRepository(NeueVoxContext context):base(context){}


  public async Task<IEnumerable<Evaluation>> GetClassEvaluationsForStudentAsync(Guid classId, Guid studentId)
  {
    var evaluationGrade = await DbContext.Evaluations
      .AsNoTracking()
      .Where(e => e.ClassId == classId)
      .Include(e => e.Grades.Where(g => g.StudentId == studentId))
      .ToListAsync();

    return evaluationGrade;
  }

  public async Task<IEnumerable<Evaluation>> GetAllEvaluationsForStudentAsync(Guid studentId)
  {
    var evaluations = await DbContext.Evaluations
      .AsNoTracking()
      .Include(e=>e.Class)
        .ThenInclude(c=>c.Course)
      .Where(e=>e.Class.StudentClasses.Any(sc=>sc.StudentId == studentId))
      .OrderBy(e=>e.DueDate)
      .ToListAsync();

    return evaluations;
  }
}
