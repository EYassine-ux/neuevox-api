using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IEvaluationService : IBaseService<Evaluation>
{
  Task<IEnumerable<Evaluation>> GetClassEvaluationsForStudentAsync(Guid classId,Guid studentId);
  Task<IEnumerable<EvaluationDashboardDTO>> GetAllEvaluationsForStudentAsync(Guid studentId);
  Task<EvaluationDetailResponseDTO?> GetEvaluationDetailAsync(Guid evaluationId);

  Task<Evaluation> AddEvaluationAsync(AddEvaluationDTO evaluation);
  Task<Evaluation?> UpdateEvaluationAsync(AddEvaluationDTO evaluation, Guid id);
}

public class EvaluationService : BaseService<Evaluation>,IEvaluationService
{
  private readonly IEvaluationRepository _evaluationRepository;

  public EvaluationService(IEvaluationRepository evaluationRepository) : base(evaluationRepository)
  {
    _evaluationRepository = evaluationRepository;
  }

  public async Task<IEnumerable<Evaluation>> GetClassEvaluationsForStudentAsync(Guid classId, Guid studentId)
  {
    return await _evaluationRepository.GetClassEvaluationsForStudentAsync(classId, studentId);
  }

  public async Task<IEnumerable<EvaluationDashboardDTO>> GetAllEvaluationsForStudentAsync(Guid studentId)
  {
    var evaluations = await _evaluationRepository.GetAllEvaluationsForStudentAsync(studentId);
    var dashboard = evaluations.Select(e => new EvaluationDashboardDTO
    {
      EvaluationId = e.EvaluationId,
      Title = e.EvaluationTitle,
      CourseName = e.Class?.Course?.CourseTitle ?? "Inconnu",
      EvaluationType =  e.EvaluationType,
      Weight = e.Weight,
      DueDate = e.DueDate,
    }).ToList();
    return dashboard;
  }

  public async Task<EvaluationDetailResponseDTO?> GetEvaluationDetailAsync(Guid evaluationId)
  {
    var raw = await _evaluationRepository.GetByIdAsync(evaluationId,
      e => e.Class,
      e => e.Class.Course);

    if (raw == null) return null;

    return new EvaluationDetailResponseDTO
    {
      EvaluationId = raw.EvaluationId,
      Title = raw.EvaluationTitle,
      CourseName = raw.Class?.Course?.CourseTitle ?? "Cours inconnu",
      EvaluationType = raw.EvaluationType.ToString(),
      Weight = raw.Weight,
      DueDate = raw.DueDate,
      Description = raw.Description,
      StartTime = raw.StartDate?.ToString("HH:mm"),
      EndTime = raw.EndDate?.ToString("HH:mm")
    };


  }

  public async Task<Evaluation> AddEvaluationAsync(AddEvaluationDTO evaluation)
  {
    var newEvaluation = new Evaluation
    {
      ClassId = evaluation.ClassId,
      EvaluationTitle = evaluation.EvaluationTitle,
      Weight = evaluation.Weight,
      EvaluationType = evaluation.EvaluationType,
      MaxScore = evaluation.MaxScore,
      DueDate = evaluation.DueDate
    };
    return await _evaluationRepository.CreateAsync(newEvaluation);
  }

  public async Task<Evaluation?> UpdateEvaluationAsync(AddEvaluationDTO evaluation, Guid id)
  {
    var oldEvaluation = await _evaluationRepository.GetByIdAsync(id);
    if (oldEvaluation == null) return null;

    oldEvaluation.ClassId = evaluation.ClassId;
    oldEvaluation.EvaluationTitle = evaluation.EvaluationTitle;
    oldEvaluation.EvaluationType = evaluation.EvaluationType;
    oldEvaluation.Weight = evaluation.Weight;
    oldEvaluation.MaxScore = evaluation.MaxScore;
    oldEvaluation.DueDate = evaluation.DueDate;

    return await _evaluationRepository.UpdateAsync(oldEvaluation,id);
  }

}
