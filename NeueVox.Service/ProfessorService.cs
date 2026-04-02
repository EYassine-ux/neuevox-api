using Microsoft.AspNetCore.Identity;
using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Model.NeuevoxModel.enums;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IProfessorService : IBaseService<Professor>
{
  Task<IEnumerable<ProfessorResponseDTO>?> GetByDepartement(string depart);
  Task<Professor?> AddProfessorAsync(AddProfessorDTO professordto);
  Task<Professor?> UpdateProfessorAsync(AddProfessorDTO professordto,Guid id);
  Task<ProfessorResponseDTO?> GetProfessorById(Guid id);
  Task<IEnumerable<ProfessorResponseDTO>?> GetAllProfessors();
}
public class ProfessorService : BaseService<Professor>, IProfessorService
{
    protected readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository) : base(professorRepository)
    {
      _professorRepository = professorRepository;
    }

    public async Task<IEnumerable<ProfessorResponseDTO>?> GetByDepartement(string depart)
    {
      var rawProfessors = (await _professorRepository.GetByDepartement(depart)).ToList();
      if (!rawProfessors.Any())
      {
        return new List<ProfessorResponseDTO>();
      }

      var professors = rawProfessors.Select(MapToResponseDTO).ToList();

      return professors;
    }


    public async Task<Professor?> AddProfessorAsync(AddProfessorDTO professordto)
    {
      var newProfessor = new Professor
      {
        FirstName = professordto.FirstName,
        LastName = professordto.LastName,
        SchoolEmail = professordto.SchoolEmail,
        Biography = professordto.Biography,
        PhoneNumber =  professordto.PhoneNumber,
        Coordination = professordto.Coordination,
        PasswordHash = professordto.Password,
        ProfilePictureUrl =  professordto.ProfilePictureUrl,
        Department = professordto.Department,
        OfficeNumber = professordto.OfficeNumber,

        Role = UserRole.PROFESSOR,
        HireDate = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      };
      newProfessor.PasswordHash = new PasswordHasher<User>()
        .HashPassword(newProfessor, professordto.Password);
      return await _professorRepository.CreateAsync(newProfessor);
    }

    public async Task<Professor?> UpdateProfessorAsync(AddProfessorDTO professordto, Guid id)
    {
      var oldProfessor = await _professorRepository.GetByIdAsync(id);
      if (oldProfessor == null) return null;

      oldProfessor.FirstName = professordto.FirstName;
      oldProfessor.LastName = professordto.LastName;
      oldProfessor.SchoolEmail = professordto.SchoolEmail;
      oldProfessor.Biography = professordto.Biography;
      oldProfessor.PhoneNumber = professordto.PhoneNumber;
      oldProfessor.ProfilePictureUrl = professordto.ProfilePictureUrl;
      oldProfessor.Coordination = professordto.Coordination;
      oldProfessor.PasswordHash = professordto.Password;
      oldProfessor.Department = professordto.Department;
      oldProfessor.OfficeNumber = professordto.OfficeNumber;

      if (!string.IsNullOrWhiteSpace(professordto.Password))
      {
        oldProfessor.PasswordHash = new PasswordHasher<User>()
          .HashPassword(oldProfessor, professordto.Password);
      }

      return await _professorRepository.UpdateAsync(oldProfessor,id);
    }

    public async Task<ProfessorResponseDTO?> GetProfessorById(Guid id)
    {
      var rawProfessors = await _professorRepository.GetByIdAsync(id);

      if(rawProfessors == null) return null;

      var professor = MapToResponseDTO(rawProfessors);

      return professor;
    }

    public async Task<IEnumerable<ProfessorResponseDTO>?> GetAllProfessors()
    {
      var rawProfessors = await _professorRepository.GetAllAsync();

      var professors = rawProfessors.Select(MapToResponseDTO).ToList();

      return professors;
    }

    private ProfessorResponseDTO MapToResponseDTO(Professor p)
    {
      return new ProfessorResponseDTO
      {
        ProfessorId = p.UserId,
        FirstName = p.FirstName,
        LastName = p.LastName,
        SchoolEmail = p.SchoolEmail,
        Biography = p.Biography,
        PhoneNumber = p.PhoneNumber,
        ProfilePictureUrl = p.ProfilePictureUrl,
        coordination = p.Coordination,
        Department = p.Department,
        OfficeNumber = p.OfficeNumber
      };
    }
    }


