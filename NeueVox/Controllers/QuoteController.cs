  using Microsoft.AspNetCore.Mvc;
  using NeueVox.Model.DTOs;
  using NeueVox.Model.NeuevoxModel.MongoDb;
  using NeueVox.Service.MongoDbService;

  namespace NeueVox.Controllers;


  [Route("api/[controller]")]
  [ApiController]
  public class QuoteController : ControllerBase
  {
    private readonly IQuoteService _quoteService;

    public QuoteController(IQuoteService quoteService)
    {
      _quoteService = quoteService;
    }

    [HttpGet(Name = "GetAllQuotes")]
    public async Task<ActionResult<List<Quote>>> Get()
    {
      return Ok(await _quoteService.GetAllQuotesAsync());
    }

    [HttpGet("daily", Name = "GetDailyQuotes")]
    public async Task<ActionResult<List<Quote>>> GetDailyQuotes()
    {
      var dailyQuotes = await _quoteService.GetDailyQuoteAsync();
      return Ok(dailyQuotes);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddQuotesDTO newQuote)
    {
      var createdQuote = await _quoteService.AddQuoteAsync(newQuote);
      if (createdQuote == null) return BadRequest("Quote text and author cannot be empty.");

      return CreatedAtAction(nameof(Get), new { id = createdQuote.Id }, createdQuote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] AddQuotesDTO updatedQuote)
    {
      await _quoteService.UpdateQuoteAsync(id, updatedQuote);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _quoteService.DeleteQuoteAsync(id);
      return NoContent();
    }

  }
