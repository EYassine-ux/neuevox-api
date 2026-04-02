using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IStudentClassService : IBaseService<StudentClass>
{
  Task<StudentClass> AddStudentClass(AddStudentClassDTO studentClass);
  Task<StudentClass?> UpdateStudentClass(AddStudentClassDTO studentClass, Guid id);
}

public class StudentClassService : BaseService<StudentClass>,IStudentClassService
{
  private readonly IStudentClassRepository _studentClassRepository;

  public StudentClassService(IStudentClassRepository studentClassRepository) : base(studentClassRepository)
  {
    _studentClassRepository = studentClassRepository;
  }


  public async Task<StudentClass> AddStudentClass(AddStudentClassDTO studentClass)
  {
    var newStudentClass = new StudentClass
    {
      ClassId = studentClass.ClassId,
      StudentId = studentClass.StudentId,
      FinalGrade = studentClass.FinalGrade,
      ClassStatus = studentClass.ClassStatus
    };
    return await _studentClassRepository.CreateAsync(newStudentClass);
  }

  public async Task<StudentClass?> UpdateStudentClass(AddStudentClassDTO studentClass, Guid id)
  {
    var oldStudentClass = await _studentClassRepository.GetByIdAsync(id);
    if (oldStudentClass is null) return null;

    oldStudentClass.ClassId = studentClass.ClassId;
    oldStudentClass.StudentId = studentClass.StudentId;
    oldStudentClass.FinalGrade = studentClass.FinalGrade;
    oldStudentClass.ClassStatus = studentClass.ClassStatus;

    return await _studentClassRepository.UpdateAsync(oldStudentClass,id);

  }
}
