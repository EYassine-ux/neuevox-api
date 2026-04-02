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
public class ProfessorController : ControllerBase
{
  private readonly IProfessorService _professorService;

  public ProfessorController(IProfessorService professorService)
  {
    _professorService = professorService;
  }

  [HttpGet]
  public async Task<IActionResult> GetProfessors()
  {
    var professors = await _professorService.GetAllProfessors();
    return Ok(professors);
  }

  [HttpGet("{professorId:guid}")]
  public async Task<ActionResult<ProfessorResponseDTO>> GetProfessorById([FromRoute] Guid professorId)
  {
    var professor = await _professorService.GetProfessorById(professorId);
    return Ok(professor);
  }
  [Authorize(Roles = "ADMIN")]
  [HttpDelete("{professorId:guid}")]
  public async Task<IActionResult> DeleteProfessor([FromRoute] Guid professorId)
  {
    var professor = await _professorService.DeleteAsync(professorId);
    if(!professor) return NotFound();
    return Ok(professor);
  }

  [Authorize(Roles = "ADMIN")]
  [HttpPost]
  public async Task<IActionResult> AddProfessor([FromBody] AddProfessorDTO professordto)
  {

    var professor = await _professorService.AddProfessorAsync(professordto);
    if(professor == null) return BadRequest(professordto);
    return Ok(professordto);
  }
  [Authorize(Roles = "ADMIN")]
  [HttpPut("{professorId:guid}")]
  public async Task<IActionResult> UpdateProfessor([FromBody] AddProfessorDTO professordto, Guid professorId)
  {

    var updatedProfessor = await _professorService.UpdateProfessorAsync(professordto, professorId);
    if(updatedProfessor is null) return NotFound();
    return Ok(updatedProfessor);
  }

  [HttpGet("depart/search")]
  public async Task<ActionResult<List<ProfessorResponseDTO>>> GetByDepartment([FromQuery] string depart)
  {
    if (string.IsNullOrWhiteSpace(depart))
    {
      return BadRequest("Department name is required for searching");
    }
    var professors = await _professorService.GetByDepartement(depart);
    return Ok(professors);
  }

}
