using NewsletterService.Services;
using Microsoft.AspNetCore.Mvc;
using Messages;

namespace NewsletterService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsletterController(NewsletterManager newsletterManager) : ControllerBase
{

    [HttpGet("article")]
    public async Task<IActionResult> GetDailyArticle()
    {
        ArticleDto? article = await newsletterManager.GetDailyArticleAsync();
        if (article == null) return NotFound();

        return Ok(article);
    }

    [HttpGet("send")]
    public async Task<bool> SendDailyNewsletter()
    {
        //sending daily newsletter
        return true;
    }
}