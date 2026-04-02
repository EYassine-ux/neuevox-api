using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Service;

namespace NeueVox.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DocumentController(IDocumentService documentService) : ControllerBase
{
  private readonly IDocumentService _documentService = documentService;

  [HttpGet]
  public async Task<ActionResult<List<DocumentResponseDTO>>> GetAllDocuments()
  {
    var documents = await _documentService.GetAllDocuments();
    return Ok(documents);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<DocumentResponseDTO>> GetDocument(Guid id)
  {
    var document = await _documentService.GetDocumentById(id);
    if (document == null)
    {
      return NotFound();
    }
    return Ok(document);
  }

  [Authorize(Roles = "ADMIN,PROFESSOR")]
  [HttpPost]
  public async Task<IActionResult> CreateDocument([FromBody] AddDocumentDTO documentDTO)
  {
    var document = await _documentService.AddDocument(documentDTO);
    if (document == null)
    {
      return Conflict("Document already exists");
    }
    return CreatedAtAction(nameof(CreateDocument), document);
  }

  [Authorize(Roles = "ADMIN,PROFESSOR")]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateDocument([FromBody] AddDocumentDTO documentDTO, Guid id)
  {
    var document = await _documentService.UpdateDocument(documentDTO, id);
    if(document == null) return NotFound();
    return NoContent();
  }

  [Authorize(Roles = "ADMIN,PROFESSOR")]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteDocument(Guid id)
  {
    bool deleted = await _documentService.DeleteAsync(id);
    if (deleted) return NoContent();
    return NotFound();
  }
}
