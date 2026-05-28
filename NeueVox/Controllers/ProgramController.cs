using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramController : ControllerBase
{
  private readonly IProgramService _programService;

  public ProgramController(IProgramService programService)
  {
    _programService = programService;
  }

  [HttpGet(Name = "GetAllPrograms")]
  public async Task<IActionResult> GetAllPrograms()
  {
    var programs = await _programService.GetAllAsync();
    return Ok(programs);
  }

  [HttpGet("{programId:guid}", Name = "GetProgramById")]
  public async Task<IActionResult> GetGradeById([FromRoute] Guid programId)
  {
    var dashbord = await _programService.GetByIdAsync(programId);
    return Ok(dashbord);
  }

  [HttpPost]
  public async Task<IActionResult> CreateProgram([FromBody] AddProgramDTO program)
  {
    var newProgram = await _programService.AddProgramAsync(program);
    if (newProgram == null) return BadRequest();
    return Ok(newProgram);
  }

  [HttpPut("{programId:guid}")]
  public async Task<IActionResult> UpdateProgram([FromBody] AddProgramDTO program, Guid programId)
  {
    var updatedProgram = await _programService.UpdateProgramAsync(program, programId);
    if (updatedProgram == null) return BadRequest();
    return Ok(updatedProgram);
  }

  [HttpDelete("{programId:guid}")]
  public async Task<IActionResult> DeleteProgramById([FromRoute] Guid programId)
  {
    var program = await _programService.DeleteAsync(programId);
    if (program == null) return NotFound();
    return Ok(program);
  }
}
