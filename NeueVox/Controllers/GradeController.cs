using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
  private readonly IGradeService _gradesService;

  public GradeController(IGradeService gradesService)
  {
    _gradesService = gradesService;
  }

  [HttpGet(Name = "GetAllGrades")]
  public async Task<IActionResult> GetAllGrade()
  {
    var grades = await _gradesService.GetAllAsync();
    return Ok(grades);
  }

  [HttpGet("{gradeId:guid}", Name = "GetGradeById")]
  public async Task<IActionResult> GetGradeById([FromRoute] Guid gradeId)
  {
    var dashbord = await _gradesService.GetByIdAsync(gradeId);
    return Ok(dashbord);
  }

  [HttpPost]
  public async Task<IActionResult> CreateGrade([FromBody] AddGradeDTO grade)
  {
    var newGrade = await _gradesService.AddGradeAsync(grade);
    if (newGrade == null) return BadRequest();
    return Ok(newGrade);
  }

  [HttpPut("{gradeId:guid}")]
  public async Task<IActionResult> UpdateGrade([FromBody] AddGradeDTO grade, Guid gradeId)
  {
    var udpdateGrade = await _gradesService.UpdateGradeAsync(grade, gradeId);
    if (udpdateGrade == null) return BadRequest();
    return Ok(udpdateGrade);
  }

  [HttpDelete("{gradeId:guid}")]
  public async Task<IActionResult> DeleteGradeById([FromRoute] Guid gradeId)
  {
    var Grade = await _gradesService.DeleteAsync(gradeId);
    if (Grade == null) return NotFound();
    return Ok(Grade);
  }
}
