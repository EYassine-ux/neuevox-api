using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel.MongoDb;
using NeueVox.Service.MongoDbService;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
  private readonly IAnnouncementService _announcementService;

  public AnnouncementController(IAnnouncementService announcementService)
  {
    _announcementService = announcementService;
  }

  [HttpGet]
  public async Task<ActionResult<List<Announcement>>> Get()
  {
    return Ok(await _announcementService.GetAllAnnouncementsAsync());
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] AddAnnouncementDTO newAnnouncementDto)
  {
    var createdAnnouncement = await _announcementService.AddAnnouncementAsync(newAnnouncementDto);
    if (createdAnnouncement == null) return BadRequest("Title and Message cannot be empty.");

    return CreatedAtAction(nameof(Get), new { id = createdAnnouncement.Id }, createdAnnouncement);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Put(string id, [FromBody] AddAnnouncementDTO updatedAnnouncementDto)
  {
    await _announcementService.UpdateAnnouncementAsync(id, updatedAnnouncementDto);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    await _announcementService.DeleteAnnouncementAsync(id);
    return NoContent();
  }
}
