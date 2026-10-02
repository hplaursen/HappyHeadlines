using PublisherService.Services;
using Microsoft.AspNetCore.Mvc;
using Messages;

namespace PublisherService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublisherController(PublisherManager publisherManager) : ControllerBase
{
    [HttpPost("article")]
    public async Task<IActionResult> PublishArticle([FromBody] ArticleDto article)
    {
        await publisherManager.PublishArticleAsync(article);
        return Accepted();
    }
}