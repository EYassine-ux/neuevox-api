using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Repository;
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


    [HttpGet]
    public async Task<IActionResult> GetEvaluations()
    {
      var evaluations = await  _evaluationService.GetAllAsync();
      return Ok(evaluations);
    }

    [HttpGet("{evaluationId:guid}")]
    public async Task<ActionResult<EvaluationDetailResponseDTO>> GetEvaluationDetail(Guid evaluationId)
    {
      var evaluation = await _evaluationService.GetEvaluationDetailAsync(evaluationId);

      if (evaluation == null) return NotFound();

      return Ok(evaluation);
    }


}
