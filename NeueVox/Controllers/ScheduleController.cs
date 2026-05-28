using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
  private readonly IScheduleService _scheduleService;
  public ScheduleController(IScheduleService scheduleService)
  {
    _scheduleService = scheduleService;
  }

  [HttpGet(Name = "GetAllSchedules")]
  public async Task<IActionResult> GetAll()
  {
    var schedules = await _scheduleService.GetAllAsync();
    return Ok(schedules);
  }

  [HttpGet("{id:guid}", Name = "GetScheduleById")]
  public async Task<IActionResult> GetById(Guid id)
  {
    var schedule = await _scheduleService.GetByIdAsync(id);
    if (schedule == null)
    {
      return NotFound($"Schedule with ID {id} was not found.");
    }
    return Ok(schedule);
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] AddScheduleDTO dto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var createdSchedule = await _scheduleService.AddSchedule(dto);

    return CreatedAtAction(nameof(GetById), new { id = createdSchedule.ScheduleId }, createdSchedule);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> Put(Guid id, [FromBody] AddScheduleDTO dto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var updatedSchedule = await _scheduleService.UpdateSchedule(dto, id);
    if (updatedSchedule == null)
    {
      return NotFound($"Schedule with ID {id} was not found.");
    }

    return Ok(updatedSchedule);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var existingSchedule = await _scheduleService.GetByIdAsync(id);
    if (existingSchedule == null)
    {
      return NotFound($"Schedule with ID {id} was not found.");
    }

    await _scheduleService.DeleteAsync(id);
    return NoContent();
  }
}
