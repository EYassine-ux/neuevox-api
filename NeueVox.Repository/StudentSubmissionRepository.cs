using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IStudentSubmissionRepository : IBaseRepository<StudentSubmission>
{
  Task<IEnumerable<StudentSubmission>?> GetAllSubmissionsForStudent(Guid studentId);

}

public class StudentSubmissionRepository : BaseRepository<StudentSubmission>,IStudentSubmissionRepository
{
  public StudentSubmissionRepository(NeueVoxContext dbContext) : base(dbContext)
  {
  }

  public async Task<IEnumerable<StudentSubmission>?> GetAllSubmissionsForStudent(Guid studentId)
  {
    var submission = await DbContext.StudentSubmissions
      .AsNoTracking()
      .Where(ss => ss.StudentId == studentId)
      .ToListAsync();

    return submission;
  }
}
