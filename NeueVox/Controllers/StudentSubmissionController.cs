using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentSubmissionController : ControllerBase
{
  private readonly IStudentSubmissionService _studentSubmissionService;

  public StudentSubmissionController(IStudentSubmissionService studentSubmissionService)
  {
    _studentSubmissionService = studentSubmissionService;
  }

  [HttpGet(Name = "GetAllSubmissions")]
  public async Task<ActionResult<List<StudentSubmissionResponse>>> GetAllSubmissions()
  {
    var submissions = await _studentSubmissionService.GetAllSubmissions();
    return Ok(submissions);
  }

  [HttpGet("{id}", Name = "GetSubmissionById")]
  public async Task<ActionResult<StudentSubmissionResponse>> GetSubmissionById([FromRoute] Guid id)
  {
    var submission = await _studentSubmissionService.GetByIdAsync(id);
    if (submission == null) return NotFound();
    return Ok(submission);
  }
  [HttpPost]
  public async Task<IActionResult> CreateSubmission([FromBody] AddStudentSubmission studentSubmission)
  {
    var submission = await _studentSubmissionService.AddSubmission(studentSubmission);

    return CreatedAtAction(
      nameof(GetSubmissionById),
      new { id = submission.Id },
      submission
    );
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateSubmission([FromRoute] Guid id,
    [FromBody] AddStudentSubmission studentSubmission)
  {
    var updatedSubmission = await _studentSubmissionService.UpdateSubmission(studentSubmission, id);
    if (updatedSubmission == null) return NotFound();
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteSubmission([FromRoute] Guid id)
  {
    bool deleted = await _studentSubmissionService.DeleteAsync(id);
    if (deleted) return NoContent();
    return NotFound();
  }
}
