using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IEvaluationService _evaluationService;
    private readonly IClassService _classService;
    private readonly IScheduleService _scheduleService;
    private readonly IGradeService _gradeService;
    private readonly IDocumentService _documentService;
  public StudentController(IStudentService studentService, IEvaluationService evaluationService,
                           IClassService classService,IScheduleService scheduleService, IGradeService gradeService,IDocumentService documentService)
  {
    _studentService = studentService;
    _evaluationService = evaluationService;
    _classService = classService;
    _scheduleService =  scheduleService;
    _gradeService = gradeService;
    _documentService = documentService;
  }

  [HttpGet("email/{email}",Name = "GetStudentByEmail")]
  public async Task<ActionResult<StudentResponseDTO>> GetStudentByEmail(string email)
  {
    var student = await _studentService.GetStudentByEmail(email);
    return Ok(student);
  }

  [HttpGet("{studentId:guid}",Name = "GetStudentById")]
  public async Task<ActionResult<StudentResponseDTO>> GetStudentById([FromRoute] Guid studentId)
  {
    var student = await _studentService.GetStudentByIdAsync(studentId);
    return Ok(student);
  }

  [HttpGet(Name = "GetAllStudents")]
  public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudents()
  {
    var students = await _studentService.GetAllStudentsAsync();

    return Ok(students);
  }

  [HttpGet("{studentId:guid}/evaluations",Name = "GetAllEvaluationsForStudent")]
  public async Task<ActionResult<List<EvaluationDashboardDTO>>> GetAllEvaluationForStudent([FromRoute] Guid studentId)
  {
    var dashboard = await _evaluationService.GetAllEvaluationsForStudentAsync(studentId);

    return Ok(dashboard);
  }
  [HttpGet("{studentId:guid}/classes",Name = "GetAllClassesForStudent")]
  public async Task<ActionResult<List<ClassResponseDTO>>> GetAllClassesForStudent(Guid studentId)
  {
    var classes = await _classService.GetAllClassesForStudent(studentId);

    return Ok(classes);
  }

  [HttpGet("{studentId:guid}/schedules",Name = "GetAllSchedulesForStudent")]
  public async Task<ActionResult<List<ScheduleResponseDTO>>> GetAllSchedulesForStudent(Guid studentId)
  {
    var schedules = await _scheduleService.GetScheduleForStudent(studentId);

    return Ok(schedules);
  }


  [HttpGet("{studentId:guid}/performance",Name = "GetStudentPerformance")]
  public async Task<ActionResult<StudentPerformanceDTO>> GetStudentPerformance(Guid studentId)
  {
    var performance = await _studentService.GetStudentPerformanceByStudentIdAsync(studentId);

    if (performance == null)
    {
      return NotFound($"Performance data not found for student with ID: {studentId}");
    }
    return Ok(performance);
  }

  [HttpDelete("{studentId:guid}")]
  public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
  {
    var student = await _studentService.DeleteAsync(studentId);
    if (!student) return NotFound();
    return Ok(student);
  }

  [HttpGet("{studentId:guid}/classes/{classId:guid}/grades",Name = "GetGradesForStudentClass")]
  public async Task<ActionResult<List<GradesClassResponseDTO>>> GetGradesForStudentClass(Guid studentId, Guid classId)
  {
    var grades = await _gradeService.GetAllGradesForStudentClass(studentId, classId);
    return Ok(grades);
  }

  [HttpPost]
  public async Task<IActionResult> CreateStudent([FromBody] AddStudentDto student)
  {
    var createdStudent = await _studentService.RegisterStudentAsync(student);

    return Ok(createdStudent);
  }

  [HttpPut("{studentId:guid}")]
  public async Task<IActionResult> UpdateStudent([FromBody] AddStudentDto student, Guid studentId)
  {

    var updatedStudent = await _studentService.UpdateStudentAsync(student, studentId);
    if (updatedStudent == null) return BadRequest();
    return Ok(updatedStudent);
  }

  [HttpGet("{studentId:guid}/classes/{classId:guid}/documents",Name = "GetDocumentsForStudentClass")]
  public async Task<ActionResult<List<DocumentResponseDTO>>> GetDocumentsForStudentClass(Guid studentId, Guid classId)
  {
    var documents = await _documentService.GetAllDocumentsForClass(classId);
    return Ok(documents);
  }

}
