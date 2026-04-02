using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IGradeService : IBaseService<Grade>
{
  Task<Grade> AddGradeAsync(AddGradeDTO grade);
  Task<Grade?> UpdateGradeAsync(AddGradeDTO grade,Guid id);
}

public class GradeService : BaseService<Grade>,IGradeService
{
  private readonly IGradeRepository _gradeRepository;

  public GradeService(IGradeRepository gradeRepository) : base(gradeRepository)
  {
    _gradeRepository = gradeRepository;
  }


  public async Task<Grade> AddGradeAsync(AddGradeDTO grade)
  {
    var newGrade = new Grade
    {
      EvaluationId = grade.EvaluationId,
      StudentId = grade.StudentId,
      Score = grade.Score,
    };
    return await _gradeRepository.CreateAsync(newGrade);
  }

  public async Task<Grade?> UpdateGradeAsync(AddGradeDTO grade, Guid id)
  {
    var oldGrade = await _gradeRepository.GetByIdAsync(id);
    if (oldGrade == null) return null;
    oldGrade.EvaluationId = grade.EvaluationId;
    oldGrade.StudentId = grade.StudentId;
    oldGrade.Score = grade.Score;

    return await _gradeRepository.UpdateAsync(oldGrade,id);
  }
}
