using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;


public interface ICourseService : IBaseService<Course>
{
  Task<Course> AddCourseAsync(AddCourseDTO course);
  Task<Course?> UpdateCourseAsync(AddCourseDTO course, Guid id);

}
public class CourseService : BaseService<Course>,ICourseService
{
  private readonly ICourseRepository _courseRepository;
  public CourseService(ICourseRepository repository) : base(repository)
  {
    _courseRepository = repository;
  }

  public async Task<Course> AddCourseAsync(AddCourseDTO course)
  {
    var newCourse = new Course
    {
      CourseCode = course.CourseCode,
      CourseTitle = course.CourseTitle
    };

    return await _courseRepository.CreateAsync(newCourse);

  }

  public async Task<Course?> UpdateCourseAsync(AddCourseDTO course, Guid id)
  {
    var oldCourse = await _courseRepository.GetByIdAsync(id);
    if(oldCourse == null) return null;

    oldCourse.CourseCode = course.CourseCode;
    oldCourse.CourseTitle = course.CourseTitle;

    return await _courseRepository.UpdateAsync(oldCourse,id);


  }


}
