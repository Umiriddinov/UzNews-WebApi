using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UzNews.WebApi.Services.Dtos;
using UzNews.WebApi.Services.Interfaces;
using UzNews.WebApi.Services.Validators.NewsFolder;
using UzNews.WebApi.Utils;

namespace UzNews.WebApi.Controllers;

[Route("api/news")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly int maxPageSize = 30;
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        this._newsService = newsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] NewsCreateDto dto)
    {
        var createValidator = new NewsCreateValidator();
        var result = createValidator.Validate(dto);
        if (result.IsValid) return Ok(await _newsService.CreateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _newsService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(long newsId, [FromForm] NewsUpdateDto dto)
    {
        var updateValidator = new NewsUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _newsService.UpdateAsync(newsId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string search, int page = 1)
        => Ok(await _newsService.SearchAsync(new PaginationParams(page, maxPageSize),search));

    [HttpDelete("{newsId}")]
    public async Task<IActionResult> DeleteAsync(long newsId)
        => Ok(await _newsService.DeleteAsync(newsId));
}
