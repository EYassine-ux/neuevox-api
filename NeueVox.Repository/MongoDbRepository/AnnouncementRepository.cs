using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel.Context;
using NeueVox.Model.NeuevoxModel.MongoDb;

namespace NeueVox.Repository.MongoDbRepository;

public interface IAnnouncementRepository
{
  Task<List<Announcement>> GetAllAsync();
  Task CreateAsync(Announcement newAnnouncement);
  Task UpdateAsync(string id, Announcement updatedAnnouncement);
  Task DeleteAsync(string id);
}
public class AnnouncementRepository : IAnnouncementRepository
{
  private readonly MongoDbContext _context;

  public AnnouncementRepository(MongoDbContext context)
  {
    _context = context;
  }
  public async Task<List<Announcement>> GetAllAsync()
  {
    return await _context.Announcements.AsNoTracking().ToListAsync();
  }

  public async Task CreateAsync(Announcement newAnnouncement)
  {
    _context.Announcements.Add(newAnnouncement);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateAsync(string id, Announcement updatedAnnouncement)
  {
    var existing = await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);
    if (existing != null)
    {
      existing.Title = updatedAnnouncement.Title;
      existing.Message = updatedAnnouncement.Message;

      await _context.SaveChangesAsync();
    }
  }

  public async Task DeleteAsync(string id)
  {
    var announcement = await _context.Announcements.FirstOrDefaultAsync(a => a.Id == id);
    if (announcement != null)
    {
      _context.Announcements.Remove(announcement);
      await _context.SaveChangesAsync();
    }
  }
}
