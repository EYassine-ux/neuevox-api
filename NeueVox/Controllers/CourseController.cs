using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Service;

namespace NeueVox.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
  private readonly ICourseService _courseService;
  private readonly IClassService _classService;
  public CourseController(ICourseService courseService,IClassService classService)
  {
    _courseService = courseService;
    _classService = classService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllCourses()
  {
    var courses = await _courseService.GetAllAsync();
    return Ok(courses);
  }
  [Authorize(Roles = "ADMIN")]
  [HttpPost]
  public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO courseDto)
  {
    var course = await _courseService.AddCourseAsync(courseDto);
    return Ok(course);
  }

  [HttpGet("{courseId:guid}/classes")]
  public async Task<ActionResult<List<ClassResponseDTO>>> GetClassesByCourseIdAsync([FromRoute] Guid courseId)
  {
      var classes = await _classService.GetAllClassesForCourse(courseId);
      return Ok(classes);
  }
}
