using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EvaluationController : ControllerBase
{
  private readonly IEvaluationService _evaluationService;

  public EvaluationController(IEvaluationService evaluationService)
  {
    _evaluationService = evaluationService;
  }

  [HttpGet(Name = "GetEvaluations")]
  public async Task<IActionResult> GetEvaluations()
  {
    var evaluations = await _evaluationService.GetAllAsync();
    return Ok(evaluations);
  }

  [HttpGet("{evaluationId:guid}", Name = "GetEvaluationById")]
  public async Task<ActionResult<EvaluationDetailResponseDTO>> GetEvaluationDetail(Guid evaluationId)
  {
    var evaluation = await _evaluationService.GetEvaluationDetailAsync(evaluationId);

    if (evaluation == null) return NotFound();

    return Ok(evaluation);
  }

  [HttpPost(Name = "AddEvaluation")]
  public async Task<IActionResult> AddEvaluation([FromBody] AddEvaluationDTO evaluationDto)
  {
    var newEvaluation = await _evaluationService.AddEvaluationAsync(evaluationDto);
    return Ok(newEvaluation);
  }

  [HttpPut("{evaluationId:guid}", Name = "UpdateEvaluation")]
  public async Task<IActionResult> UpdateEvaluation(Guid evaluationId, [FromBody] AddEvaluationDTO evaluationDto)
  {
    var updatedEvaluation = await _evaluationService.UpdateEvaluationAsync(evaluationDto, evaluationId);

    if (updatedEvaluation == null) return NotFound();

    return Ok(updatedEvaluation);
  }

  [HttpDelete("{evaluationId:guid}", Name = "DeleteEvaluation")]
  public async Task<IActionResult> DeleteEvaluation(Guid evaluationId)
  {
    await _evaluationService.DeleteAsync(evaluationId);
    return Ok();
  }






}
