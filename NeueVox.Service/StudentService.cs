using Microsoft.AspNetCore.Identity;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.enums;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IStudentService : IBaseService<Student>
{
  Task<StudentResponseDTO> GetStudentByEmail(string email);
  Task<Student> RegisterStudentAsync(AddStudentDto studentdto);
  Task<Student?> UpdateStudentAsync(AddStudentDto studentdto, Guid id);
  Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync();
  Task<StudentResponseDTO?> GetStudentByIdAsync(Guid id);
}

public class StudentService : BaseService<Student>, IStudentService
{
  protected readonly IStudentRepository _studentRepository;
  public StudentService(IStudentRepository repository) : base(repository)
  {
    _studentRepository = repository;
  }

  public async Task<StudentResponseDTO> GetStudentByEmail(string email)
  {
    var rawStudent = await _studentRepository.GetStudentByEmail(email);

    var student = MapToResponseDTO(rawStudent);

    return student;
  }





  public async Task<Student> RegisterStudentAsync(AddStudentDto studentdto)
  {
    var student = new Student
    {
      FirstName = studentdto.FirstName,
      LastName = studentdto.LastName,
      SchoolEmail = studentdto.SchoolEmail,
      Coordination = studentdto.Coordination,
      PhoneNumber =  studentdto.PhoneNumber,
      ProfilePictureUrl =  studentdto.ProfilePictureUrl,
      PasswordHash = studentdto.Password,
      ProgramId = studentdto.ProgramId,

      Role = UserRole.STUDENT,
      AdmissionDate = DateTime.UtcNow,
      CreatedAt =  DateTime.UtcNow,
    };
    student.PasswordHash = new PasswordHasher<User>()
      .HashPassword(student, studentdto.Password);

    return await _studentRepository.CreateAsync(student);
  }

  public async Task<Student?> UpdateStudentAsync(AddStudentDto studentdto,Guid id )
  {
    var studentToUpdate = await _studentRepository.GetByIdAsync(id);
    if (studentToUpdate == null) return null;
    studentToUpdate.FirstName = studentdto.FirstName;
    studentToUpdate.LastName = studentdto.LastName;
    studentToUpdate.ProgramId = studentdto.ProgramId;
    studentToUpdate.Coordination = studentdto.Coordination;
    studentToUpdate.PhoneNumber = studentdto.PhoneNumber;
    studentToUpdate.ProfilePictureUrl = studentdto.ProfilePictureUrl;
    studentToUpdate.SchoolEmail = studentdto.SchoolEmail;

    if (!string.IsNullOrWhiteSpace(studentdto.Password))
    {
      studentToUpdate.PasswordHash = new PasswordHasher<User>()
        .HashPassword(studentToUpdate, studentdto.Password);
    }

    return await _studentRepository.UpdateAsync(studentToUpdate,id);
  }

  public async Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync()
  {
    var rawStudents = await _studentRepository.GetAllAsync(s=>s.Program);

    var students = rawStudents.Select(MapToResponseDTO).ToList();

    return students;
  }

  public async Task<StudentResponseDTO?> GetStudentByIdAsync(Guid id)
  {
    var rawStudent = await _studentRepository.GetByIdAsync(id);
    if (rawStudent == null) return null;
    var student = MapToResponseDTO(rawStudent);

    return student;
  }

  private StudentResponseDTO MapToResponseDTO(Student s)
  {
    return new StudentResponseDTO
    {
      StudentId = s.UserId,
      FirstName = s.FirstName,
      LastName = s.LastName,
      SchoolEmail = s.SchoolEmail,
      Coordination = s.Coordination,
      PhoneNumber = s.PhoneNumber,
      ProfilePictureUrl = s.ProfilePictureUrl,
      ProgramName = s.Program?.ProgramName ?? "Non spécifié",
      AdmissionDate = s.AdmissionDate,
    };
  }
}
