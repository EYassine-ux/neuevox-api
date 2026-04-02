using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IEvaluationService _evaluationService;
    private readonly IClassService _classService;
  public StudentController(IStudentService studentService, IEvaluationService evaluationService, IClassService classService)
  {
    _studentService = studentService;
    _evaluationService = evaluationService;
    _classService = classService;
  }




  [HttpGet("email/{email}")]
  public async Task<ActionResult<StudentResponseDTO>> GetStudentByEmail(string email)
  {
    var student = await _studentService.GetStudentByEmail(email);
    return Ok(student);
  }

  [HttpGet("{studentId:guid}")]
  public async Task<ActionResult<StudentResponseDTO>> GetStudentById([FromRoute] Guid studentId)
  {
    var student = await _studentService.GetStudentByIdAsync(studentId);
    return Ok(student);
  }

  [HttpGet]
  public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudents()
  {
    var students = await _studentService.GetAllStudentsAsync();

    return Ok(students);
  }

  [HttpGet("{studentId:guid}/evaluations")]
  public async Task<ActionResult<List<EvaluationDashboardDTO>>> GetAllEvaluationForStudent([FromRoute] Guid studentId)
  {
    var dashboard  = await _evaluationService.GetAllEvaluationsForStudentAsync(studentId);

    return Ok(dashboard);
  }
  [HttpGet("{studentId:guid}/classes")]
  public async Task<ActionResult<List<ClassResponseDTO>>> GetAllClassesForStudent(Guid studentId)
  {
    var classes = await _classService.GetAllClassesForStudent(studentId);

    return Ok(classes);
  }
  [Authorize(Roles = "ADMIN")]
  [HttpDelete("{studentId:guid}")]
  public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
  {
    var student = await _studentService.DeleteAsync(studentId);
    if (!student) return NotFound();
    return Ok(student);
  }

  //[HttpGet("{studentId:guid}/classes/{classId:guid}/results")]
  // public async Task<IActionResult> GetStudentResultsByClass(Guid studentId, Guid classId)
  // {
  //   var result = await _service.GetStudentResultsByClass(classId, studentId);
  //   return Ok(result);
  // }
  [Authorize(Roles = "ADMIN")]
  [HttpPost]
  public async Task<IActionResult> CreateStudent([FromBody] AddStudentDto student)
  {

    var createdStudent =  await _studentService.RegisterStudentAsync(student);

    return Ok(createdStudent);
  }
  [Authorize(Roles = "ADMIN")]
  [HttpPut("{studentId:guid}")]
  public async Task<IActionResult> UpdateStudent([FromBody] AddStudentDto student,Guid studentId)
  {

    var updatedStudent = await _studentService.UpdateStudentAsync(student, studentId);
    if (updatedStudent == null) return BadRequest();
    return Ok(updatedStudent);
  }



}
