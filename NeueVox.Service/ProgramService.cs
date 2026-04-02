using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository;

namespace NeueVox.Service;

public interface IProgramService : IBaseService<Program>
{
  Task<Program> AddProgramAsync(AddProgramDTO program);
  Task<Program?> UpdateProgramAsync(AddProgramDTO program, Guid id);
}
public class ProgramService : BaseService<Program>,IProgramService
{
  private readonly IProgramRepository _programRepository;

  public ProgramService(IProgramRepository programRepository) : base(programRepository)
  {
    _programRepository = programRepository;
  }

  public async Task<Program> AddProgramAsync(AddProgramDTO program)
  {
    var newProgram = new Program
    {
      ProgramName = program.ProgramName,
      ProgramCode = program.ProgramCode
    };

    return await _programRepository.CreateAsync(newProgram);
  }

  public async Task<Program?> UpdateProgramAsync(AddProgramDTO program, Guid id)
  {
    var oldProgram = await _programRepository.GetByIdAsync(id);
    if (oldProgram == null) return null;
    oldProgram.ProgramName = program.ProgramName;
    oldProgram.ProgramCode = program.ProgramCode;

    return await _programRepository.UpdateAsync(oldProgram,id);
  }
}
