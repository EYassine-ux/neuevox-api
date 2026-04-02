using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IStudentSubmissionService : IBaseService<StudentSubmission>
{
  Task<IEnumerable<StudentSubmissionResponse>?> GetAllSubmissionsForStudent(Guid studentId);
  Task<IEnumerable<StudentSubmissionResponse>> GetAllSubmissions();
  Task<StudentSubmissionResponse> GetSubmission(Guid submissionId);
  Task<StudentSubmissionResponse> AddSubmission(AddStudentSubmission addStudentSubmissionDTO);
  Task<StudentSubmissionResponse?> UpdateSubmission(AddStudentSubmission addStudentSubmissionDTO, Guid submissionId);
}

public class StudentSubmissionService : BaseService<StudentSubmission>,IStudentSubmissionService
{
  private readonly IStudentSubmissionRepository _studentSubmissionRepository;
  private readonly IEvaluationRepository _evaluationRepository;
  public StudentSubmissionService(IStudentSubmissionRepository repository,IEvaluationRepository evaluationRepository) : base(repository)
  {
    _studentSubmissionRepository = repository;
    _evaluationRepository = evaluationRepository;
  }

  public async Task<IEnumerable<StudentSubmissionResponse>?> GetAllSubmissionsForStudent(Guid studentId)
  {
    var submission = await _studentSubmissionRepository.GetAllSubmissionsForStudent(studentId);

    if (submission == null) return null;

    var response = submission?.Select(MapToResponse).ToList();

    return  response;
  }

  public async Task<IEnumerable<StudentSubmissionResponse>> GetAllSubmissions()
  {
    var rawSubmissions = await _studentSubmissionRepository.GetAllAsync();
    if (rawSubmissions == null) return null;
    var response = rawSubmissions.Select(MapToResponse).ToList();
    return response;

  }

  public async Task<StudentSubmissionResponse> GetSubmission(Guid submissionId)
  {
    var rawSubmission = await _studentSubmissionRepository.GetByIdAsync(submissionId);
    if (rawSubmission == null) return null;

    var response = MapToResponse(rawSubmission);
    return response;
  }

  public async Task<StudentSubmissionResponse> AddSubmission(AddStudentSubmission addStudentSubmissionDTO)
  {
    var evalution = await _evaluationRepository.GetByIdAsync(addStudentSubmissionDTO.EvaluationId);

    if (evalution == null) return null;
    var lastSubmittingDay = evalution.DueDate.AddDays(3);
    var currentSubmissionDate = DateTime.Now;

    if (currentSubmissionDate > lastSubmittingDay)
    {
      return null;
    }

    var newSubmission = new StudentSubmission
    {
      StudentId = addStudentSubmissionDTO.StudentId,
      EvaluationId = addStudentSubmissionDTO.EvaluationId,
      SubmissionDate = DateTime.UtcNow,
      FileUrl = addStudentSubmissionDTO.FileUrl
    };

    var createdSubmission = await _studentSubmissionRepository.CreateAsync(newSubmission);

    return MapToResponse(createdSubmission);

  }

  public async Task<StudentSubmissionResponse?> UpdateSubmission(AddStudentSubmission addStudentSubmissionDTO,
    Guid submissionId)
  {
    var existingSubmission = await _studentSubmissionRepository.GetByIdAsync(submissionId);
    if (existingSubmission == null) return null;

    existingSubmission.StudentId = addStudentSubmissionDTO.StudentId;
    existingSubmission.EvaluationId = addStudentSubmissionDTO.EvaluationId;
    existingSubmission.SubmissionDate = DateTime.UtcNow;
    existingSubmission.FileUrl = addStudentSubmissionDTO.FileUrl;

    var updatedSubmission = await _studentSubmissionRepository.UpdateAsync(existingSubmission, submissionId);

    return updatedSubmission == null ? null : MapToResponse(updatedSubmission);

  }



  private StudentSubmissionResponse MapToResponse(StudentSubmission ss)
  {
    return new StudentSubmissionResponse
    {
      Id = ss.SubmissionId,
      StudentId = ss.StudentId,
      EvaluationId = ss.EvaluationId,
      SubmissionDate = ss.SubmissionDate,
      FileUrl = ss.FileUrl
    };
  }


}
