using Microsoft.EntityFrameworkCore;
using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IClassRepository : IBaseRepository<Class>
{
  Task<IEnumerable<Class>> GetAllClassesForStudent(Guid studentId);
  Task<IEnumerable<Class>> GetAllClassesForCourse(Guid courseId);

}

public class ClassRepository : BaseRepository<Class>,IClassRepository
{
  public ClassRepository(NeueVoxContext  context) : base(context)
  {
  }

  public async Task<IEnumerable<Class>> GetAllClassesForStudent(Guid studentId)
  {
    var classes = await DbContext.StudentClasses
      .AsNoTracking()
      .Where(s => s.StudentId == studentId)
      .Include(sc => sc.Class)
            .ThenInclude(c => c.Professor)
      .Include(sc => sc.Class)
           .ThenInclude(c=>c.Course)
      .Select(sc => sc.Class)
      .ToListAsync();
    return classes;

  }

  public async Task<IEnumerable<Class>> GetAllClassesForCourse(Guid courseId)
  {
    var classes = await DbContext.Classes
      .AsNoTracking()
      .Where(c => c.CourseId == courseId)
      .Include(c=>c.Professor)
      .ToListAsync();

    return classes;
  }



  // public async Task<IEnumerable<ReportCardDTO>> GetStudentReportCardAsync(Guid studentId, string semester)
  // {
  //   var reportCard = await DbContext.StudentClasses
  //       .AsNoTracking()
  //       .Where(sc => sc.StudentId == studentId && sc.Class.Semester == semester)
  //       .Select( sc => new ReportCardDTO
  //       {
  //         CourseTitle = sc.Class.Course.CourseTitle,
  //         ProfessorName = sc.Class.Professor.LastName,
  //         FinalGrade = sc.FinalGrade,
  //         ClassStatus = sc.ClassStatus
  //       })
  //     .ToListAsync();
  //
  //   return reportCard;
  // }

}
