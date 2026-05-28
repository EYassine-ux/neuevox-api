using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IGradeRepository : IBaseRepository<Grade>
{
  Task<IEnumerable<Grade>> GetAllGradesForStudentWithClass(Guid studentId);
  Task<IEnumerable<Grade>> GetAllGradesForStudentClass(Guid studentId, Guid classId);
  Task<IEnumerable<Grade>> GetAllGradesForEvaluation(Guid evaluationId);
  Task<Dictionary<Guid, decimal>> GetEvaluationAverageForClass(Guid classId);

}

public class GradeRepository : BaseRepository<Grade>, IGradeRepository
{
  public GradeRepository(NeueVoxContext dbContext) : base(dbContext)
  {
  }

  public async Task<IEnumerable<Grade>> GetAllGradesForStudentWithClass(Guid studentId)
  {
    var grades = await DbContext.Grades.AsNoTracking()
      .Where(x => x.StudentId == studentId)
      .Include(x => x.Evaluation)
      .ThenInclude(e => e.Class)
      .ThenInclude(c => c.Course).ToListAsync();

    return grades;
  }

  public async Task<IEnumerable<Grade>> GetAllGradesForStudentClass(Guid studentId, Guid classId)
  {
    var grades = await DbContext.Grades.AsNoTracking()
      .Where(g => g.StudentId == studentId && g.Evaluation.ClassId == classId)
      .Include(g => g.Evaluation)
      .ToListAsync();

    return grades;
  }

  public async Task<IEnumerable<Grade>> GetAllGradesForEvaluation(Guid evaluationId)
  {
    var grades = await DbContext.Grades.AsNoTracking()
      .Where(g => g.EvaluationId == evaluationId)
      .Include(g => g.Evaluation)
      .ToListAsync();

    return grades;
  }

  public async Task<Dictionary<Guid, decimal>> GetEvaluationAverageForClass(Guid classId)
  {
    var average = await DbContext.Grades.AsNoTracking()
      .Where(g => g.Evaluation.ClassId == classId)
      .GroupBy(g => g.EvaluationId)
      .Select(group => new
      {
        EvaluationId = group.Key,
        AverageScore = group.Average(g => g.Score)
      }).ToDictionaryAsync(x => x.EvaluationId, x => x.AverageScore);

    return average;
  }
}
