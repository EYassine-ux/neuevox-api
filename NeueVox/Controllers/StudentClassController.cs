using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentClassController : ControllerBase
{
  private readonly IStudentClassService _studentClassService;

  public StudentClassController(IStudentClassService studentClassService)
  {
    _studentClassService = studentClassService;
  }

  [HttpGet(Name = "GetAllStudentClasses")]
  public async Task<IActionResult> GetAllStudentClasses()
  {
    var studentClasses = await _studentClassService.GetAllAsync();
    return Ok(studentClasses);
  }

  [HttpGet("{studentClassId:guid}", Name = "GetStudentClassById")]
  public async Task<IActionResult> GetStudentClassesById([FromRoute] Guid studentClassId)
  {
    var dashboard = await _studentClassService.GetByIdAsync(studentClassId);
    return Ok(dashboard);
  }

  [HttpPost]
  public async Task<IActionResult> CreateStudentClass([FromBody] AddStudentClassDTO studentClass)
  {
    var createStudentClasse = await _studentClassService.AddStudentClass(studentClass);
    return Ok(createStudentClasse);
  }

  [HttpPut("{studentClassId:guid}")]
  public async Task<IActionResult> UpdateStudentClass([FromBody] AddStudentClassDTO studentClass, Guid studentClassId)
  {
    var updatedStudent = await _studentClassService.UpdateStudentClass(studentClass, studentClassId);
    if (updatedStudent == null) return BadRequest();
    return Ok(updatedStudent);
  }

  [HttpDelete("{studentClassId:guid}")]
  public async Task<IActionResult> DeleteStudentClass([FromRoute] Guid studentClassId)
  {
    var StudentClass = await _studentClassService.DeleteAsync(studentClassId);
    if (StudentClass == null) return BadRequest();
    return Ok(StudentClass);
  }
}
