using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel.MongoDb;
using NeueVox.Repository.MongoDbRepository;

namespace NeueVox.Service.MongoDbService;

public interface IAnnouncementService
{
  Task<List<Announcement>> GetAllAnnouncementsAsync();
  Task<Announcement?> AddAnnouncementAsync(AddAnnouncementDTO newAnnouncementDto);
  Task UpdateAnnouncementAsync(string id, AddAnnouncementDTO updatedAnnouncementDto);
  Task DeleteAnnouncementAsync(string id);
}

public class AnnouncementService : IAnnouncementService
{
  private readonly IAnnouncementRepository _repository;

  public AnnouncementService(IAnnouncementRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<Announcement>> GetAllAnnouncementsAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<Announcement?> AddAnnouncementAsync(AddAnnouncementDTO newAnnouncementDto)
  {
    if (string.IsNullOrWhiteSpace(newAnnouncementDto.Title) || string.IsNullOrWhiteSpace(newAnnouncementDto.Message))
    {
      return null;
    }

    var announcementEntity = new Announcement
    {
      Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
      Title = newAnnouncementDto.Title,
      Message = newAnnouncementDto.Message,
      DatePosted = DateTime.UtcNow
    };

    await _repository.CreateAsync(announcementEntity);

    return announcementEntity;
  }

  public async Task UpdateAnnouncementAsync(string id, AddAnnouncementDTO updatedAnnouncementDto)
  {
    var announcementEntity = new Announcement
    {
      Id = id,
      Title = updatedAnnouncementDto.Title,
      Message = updatedAnnouncementDto.Message
    };

    await _repository.UpdateAsync(id, announcementEntity);
  }

  public async Task DeleteAnnouncementAsync(string id)
  {
    await _repository.DeleteAsync(id);
  }
}
