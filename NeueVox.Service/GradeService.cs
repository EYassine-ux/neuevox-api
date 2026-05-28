using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IGradeService : IBaseService<Grade>
{
  Task<Grade> AddGradeAsync(AddGradeDTO grade);
  Task<Grade?> UpdateGradeAsync(AddGradeDTO grade, Guid id);
  Task<IEnumerable<StudentGradesResponseDTO>?> GetAllGradesForStudentWithClass(Guid studentId);
  Task<IEnumerable<GradesClassResponseDTO>> GetAllGradesForStudentClass(Guid studentId, Guid classId);
  Task<IEnumerable<GradesClassResponseDTO>> GetAllGradesForEvaluation(Guid evaluationId);

}

public class GradeService : BaseService<Grade>, IGradeService
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

    return await _gradeRepository.UpdateAsync(oldGrade, id);
  }

  public async Task<IEnumerable<StudentGradesResponseDTO>?> GetAllGradesForStudentWithClass(Guid studentId)
  {
    var rawGrades = await _gradeRepository.GetAllGradesForStudentWithClass(studentId);

    var grades = rawGrades.Select(g => new StudentGradesResponseDTO
    {
      ClassName = g.Evaluation.Class.Course.CourseTitle,
      EvaluationTitle = g.Evaluation.EvaluationTitle,
      EvaluationGrade = g.Score,
      EvaluationWeight = g.Evaluation.Weight

    });
    return grades;
  }

  public async Task<IEnumerable<GradesClassResponseDTO>> GetAllGradesForStudentClass(Guid studentId, Guid classId)
  {
    var rawGrades = await _gradeRepository.GetAllGradesForStudentClass(studentId, classId);
    var classAverages = await _gradeRepository.GetEvaluationAverageForClass(classId);

    var grades = rawGrades.Select(g => new GradesClassResponseDTO
    {
      EvauationId = g.EvaluationId,
      EvaluationTitle =  g.Evaluation.EvaluationTitle,
      EvaluationType = g.Evaluation.EvaluationType.ToString(),
      EvaluationGrade = g.Score,
      EvaluationAverage = classAverages.GetValueOrDefault(g.EvaluationId, 0m),
      EvaluationWeight = g.Evaluation.Weight,
      DueDate = g.Evaluation.DueDate

    });

    return grades;
  }

  public async Task<IEnumerable<GradesClassResponseDTO>> GetAllGradesForEvaluation(Guid evaluationId)
  {
    var rawGrades = await _gradeRepository.GetAllGradesForEvaluation(evaluationId);

    var grades = rawGrades.Select(g => new GradesClassResponseDTO
    {
      EvauationId = g.EvaluationId,
      EvaluationTitle =  g.Evaluation.EvaluationTitle,
      EvaluationType = g.Evaluation.EvaluationType.ToString(),
      EvaluationGrade = g.Score,
      EvaluationWeight = g.Evaluation.Weight,
      DueDate = g.Evaluation.DueDate,
    });

    return grades;
  }
}
