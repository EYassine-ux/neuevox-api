  using NeueVox.Model.DTOs;
  using NeueVox.Model.DTOs.ReponseDTO;
  using NeueVox.Model.NeuevoxModel;
  using NeueVox.Model.NeuevoxModel.Context;
  using NeueVox.Repository;

  namespace NeueVox.Service;

  public interface IClassService : IBaseService<Class>
  {
    Task<Class> AddClassAsync(AddClassDTO classDto);
    Task<Class?> UpdateClassAsync(Guid id, AddClassDTO classDto);
    Task<IEnumerable<ClassResponseDTO>> GetAllClassesForCourse(Guid courseId);

    Task<IEnumerable<ClassResponseDTO>> GetAllClassesForStudent(Guid studentId);



  }
  public class ClassService : BaseService<Class>, IClassService
  {
    private readonly IClassRepository _classRepository;

    public ClassService(IClassRepository classRepository) : base(classRepository)
    {
      _classRepository = classRepository;
    }



    public async Task<Class> AddClassAsync(AddClassDTO classDto)
    {
      var newClass = new Class
      {
        ProfessorId = classDto.ProfessorId,
        CourseId = classDto.CourseId,
        ClassNumber = classDto.ClassNumber,
        Semester = classDto.Semester,
      };
      return await _classRepository.CreateAsync(newClass);
    }

    public async Task<Class?> UpdateClassAsync(Guid id, AddClassDTO classDto)
    {
      var oldClass = await _classRepository.GetByIdAsync(id);
      if(oldClass is null) return null;

      oldClass.ProfessorId = classDto.ProfessorId;
      oldClass.CourseId = classDto.CourseId;
      oldClass.ClassNumber = classDto.ClassNumber;
      oldClass.Semester = classDto.Semester;

      return await _classRepository.UpdateAsync(oldClass,id);
    }


     public async Task<IEnumerable<ClassResponseDTO>> GetAllClassesForCourse(Guid courseId)
     {
        var classes = await _classRepository.GetAllClassesForCourse(courseId);

        var responseDto = classes.Select(c => new ClassResponseDTO
        {
            ClassId  =  c.ClassId,
            ClassNumber = c.ClassNumber,
            CourseTitle = c.Course.CourseTitle,
            Semester =  c.Semester,
            ProfessorName = $"{c.Professor.FirstName} {c.Professor.LastName}",
            ProfessorOffice = c.Professor.OfficeNumber,

        }).ToList();
      return responseDto;
     }

    public async Task<IEnumerable<ClassResponseDTO>> GetAllClassesForStudent(Guid studentId)
    {
      var rawClasses = await _classRepository.GetAllClassesForStudent(studentId);

      var classes = rawClasses.Select(c => new ClassResponseDTO
      {
        ClassId = c.ClassId,
        CourseTitle = c.Course.CourseTitle,
        ClassNumber = c.ClassNumber,
        ProfessorName = $"{c.Professor.FirstName} {c.Professor.LastName}",
        ProfessorOffice = c.Professor.OfficeNumber,
        Semester = c.Semester,
      }).ToList();
      return classes;
    }
  }
