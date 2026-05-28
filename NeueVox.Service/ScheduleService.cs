using NeueVox.Model.DTOs;
using NeueVox.Model.DTOs.ReponseDTO;
using NeueVox.Model.NeuevoxModel;
using NeueVox.Repository.MongoDbRepository;

namespace NeueVox.Service;

public interface IScheduleService : IBaseService<Schedule>
{
  Task<Schedule> AddSchedule(AddScheduleDTO dto);
  Task<Schedule?> UpdateSchedule(AddScheduleDTO dto, Guid ScheduleId);
  Task<IEnumerable<ScheduleResponseDTO>> GetScheduleForStudent(Guid studentId);
  Task<IEnumerable<ScheduleResponseDTO>> GetScheduleForProfessor(Guid professorId);

}

public class ScheduleService : BaseService<Schedule>, IScheduleService
{
  private readonly IScheduleRepository _scheduleRepository;
  public ScheduleService(IScheduleRepository scheduleRepository) : base(scheduleRepository)
  {
    _scheduleRepository = scheduleRepository;
  }

  public async Task<Schedule> AddSchedule(AddScheduleDTO dto)
  {
    var schedule = new Schedule
    {
      ClassId = dto.ClassId,
      DayOfWeek = dto.DayOfWeek,
      EndTime = dto.EndTime,
      StartTime = dto.StartTime,
      RoomNumber = dto.RoomNumber
    };

    return await _scheduleRepository.CreateAsync(schedule);
  }

  public async Task<Schedule?> UpdateSchedule(AddScheduleDTO dto, Guid ScheduleId)
  {
    var existingSchdule = await _scheduleRepository.GetByIdAsync(ScheduleId);
    if (existingSchdule is null) return null;

    existingSchdule.ClassId = dto.ClassId;
    existingSchdule.DayOfWeek = dto.DayOfWeek;
    existingSchdule.EndTime = dto.EndTime;
    existingSchdule.StartTime = dto.StartTime;
    existingSchdule.RoomNumber = dto.RoomNumber;

    return await _scheduleRepository.UpdateAsync(existingSchdule, ScheduleId);
  }

  public async Task<IEnumerable<ScheduleResponseDTO>> GetScheduleForStudent(Guid studentId)
  {
    var rawSchedules = await _scheduleRepository.GetScheduleForStudent(studentId);

    var schedules = rawSchedules.Select(s => new ScheduleResponseDTO
    {
      ClassId = s.ClassId,
      ClassName = s.Class?.Course?.CourseTitle ?? "Unknown",
      StartTime = s.StartTime,
      EndTime = s.EndTime,
      DayOfWeek = s.DayOfWeek,
      RoomNumber = s.RoomNumber
    });

    return schedules;
  }

  public async Task<IEnumerable<ScheduleResponseDTO>> GetScheduleForProfessor(Guid professorId)
  {
    var rawSchedules = await _scheduleRepository.GetScheduleForProfessor(professorId);

    var schedules = rawSchedules.Select(s => new ScheduleResponseDTO
    {
      ClassId = s.ClassId,
      ClassName = s.Class?.Course?.CourseTitle ?? "Unknown",
      StartTime = s.StartTime,
      EndTime = s.EndTime,
      DayOfWeek = s.DayOfWeek,
      RoomNumber = s.RoomNumber
    });
    return schedules;
  }
}
