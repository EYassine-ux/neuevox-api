using NeueVox.Model.DTOs;
using NeueVox.Model.NeuevoxModel.MongoDb;
using NeueVox.Repository.MongoDbRepository;

namespace NeueVox.Service.MongoDbService;

public interface IQuoteService
{
  Task<List<Quote>> GetAllQuotesAsync();
  Task<Quote?> GetDailyQuoteAsync();
  Task<Quote?> AddQuoteAsync(AddQuotesDTO newQuoteDto);
  Task UpdateQuoteAsync(string id, AddQuotesDTO updatedQuote);
  Task DeleteQuoteAsync(string id);

}

public class QuoteService : IQuoteService
{
  private readonly IQuoteRepository _repository;

  public QuoteService(IQuoteRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<Quote>> GetAllQuotesAsync()
  {
    return await _repository.GetAllAsync();
  }

  public async Task<Quote?> GetDailyQuoteAsync()
  {
    var totalQuotes = await _repository.GetCountAsync();
    if (totalQuotes == 0) return null;
    var montrealZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
    var today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, montrealZone);
    int dayOfYear = today.DayOfYear;
    int quoteIndex = (int)(dayOfYear % totalQuotes);
    return await _repository.GetByIndexAsync(quoteIndex);

  }

  public async Task<Quote?> AddQuoteAsync(AddQuotesDTO newQuoteDto)
  {
    if (string.IsNullOrWhiteSpace(newQuoteDto.Text) || string.IsNullOrWhiteSpace(newQuoteDto.Author))
    {
      return null;
    }

    var quoteEntity = new Quote
    {
      Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
      Text = newQuoteDto.Text,
      Author = newQuoteDto.Author
    };

    await _repository.CreateAsync(quoteEntity);

    return quoteEntity;
  }

  public async Task UpdateQuoteAsync(string id, AddQuotesDTO updatedQuoteDto)
  {
    var quoteEntity = new Quote
    {
      Id = id,
      Text = updatedQuoteDto.Text,
      Author = updatedQuoteDto.Author
    };

    await _repository.UpdateAsync(id, quoteEntity);
  }

  public async Task DeleteQuoteAsync(string id)
  {
    await _repository.DeleteAsync(id);
  }
}
