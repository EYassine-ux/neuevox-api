using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassController : ControllerBase
{
  private readonly IClassService _classService;
  private readonly IDocumentService _documentService;

  public ClassController(IClassService classService, IDocumentService documentService)
  {
    _classService = classService;
    _documentService = documentService;
  }

  [HttpGet(Name = "GetClasses")]
  public async Task<IActionResult> GetClasses()
  {
    var classes = await _classService.GetAllAsync();
    return Ok(classes);
  }

  [HttpGet("{id:guid}",Name = "GetClassById")]
  public async Task<IActionResult> GetClassById([FromRoute] Guid id)
  {
    var classes = await _classService.GetByIdAsync(id);

    return Ok(classes);
  }

  [HttpGet("{classId:guid}/documents", Name = "GetClassDocuments")]
  public async Task<ActionResult<List<DocumentResponseDTO>>> GetClassDocuments([FromRoute] Guid classId)
  {
    var documents = await _documentService.GetAllDocumentsForClass(classId);
    return Ok(documents);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteClass([FromRoute] Guid id)
  {
    var classes = await _classService.DeleteAsync(id);
    if (!classes) return NotFound();
    return Ok(classes);
  }

  [HttpPost]
  public async Task<IActionResult> AddClass([FromBody] AddClassDTO classDto)
  {
    var addedClass = await _classService.AddClassAsync(classDto);
    if (addedClass == null) return BadRequest();
    return Ok(addedClass);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateClass(Guid id, [FromBody] AddClassDTO classDto)
  {
    var updatedClass = await _classService.UpdateClassAsync(id, classDto);
    if (updatedClass is null) return BadRequest();

    return Ok(updatedClass);
  }
}
