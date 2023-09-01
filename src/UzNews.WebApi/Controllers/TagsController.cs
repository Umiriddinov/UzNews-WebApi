using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UzNews.WebApi.Services.Interfaces;
using UzNews.WebApi.Utils;

namespace UzNews.WebApi.Controllers;

[Route("api/tags")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagsService _tagsService;

    public TagsController(ITagsService tagsService)
    {
        this._tagsService = tagsService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string search)
        => Ok(await _tagsService.SearchAsync( search));

}
