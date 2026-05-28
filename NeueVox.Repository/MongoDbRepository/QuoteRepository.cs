using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using NeueVox.Model.NeuevoxModel.Context;
using NeueVox.Model.NeuevoxModel.MongoDb;

namespace NeueVox.Repository.MongoDbRepository;

public interface IQuoteRepository
{
  Task<List<Quote>> GetAllAsync();
  Task CreateAsync(Quote newQuote);
  Task UpdateAsync(string id, Quote updatedQuote);
  Task DeleteAsync(string id);
  Task<long> GetCountAsync();
  Task<Quote?> GetByIndexAsync(int index);
}

public class QuoteRepository : IQuoteRepository
{
  private readonly MongoDbContext _context;

  public QuoteRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<List<Quote>> GetAllAsync()
  {
    return await _context.Quotes.AsNoTracking().ToListAsync();
  }

  public async Task CreateAsync(Quote newQuote)
  {
    _context.Quotes.Add(newQuote);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateAsync(string id, Quote updatedQuote)
  {
    var existingQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id);
    if (existingQuote != null)
    {
      existingQuote.Text = updatedQuote.Text;
      existingQuote.Author = updatedQuote.Author;
      await _context.SaveChangesAsync();
    }
  }

  public async Task DeleteAsync(string id)
  {
    var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id);
    if (quote != null)
    {
      _context.Quotes.Remove(quote);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<long> GetCountAsync()
  {
    return await _context.Quotes.CountAsync();  }

  public async Task<Quote?> GetByIndexAsync(int index)
  {
    return await _context.Quotes
      .AsNoTracking()
      .OrderBy(q => q.Id)
      .Skip(index)
      .FirstOrDefaultAsync();
  }
}
