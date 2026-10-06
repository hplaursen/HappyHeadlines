using ArticleService.Models;
using ArticleService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ArticleController(ArticleRepository articlesRepository) : ControllerBase
{

    [HttpGet("{continent}/{id}")]
    public async Task<IActionResult> GetArticle(int id, Continent continent)
    {
        Article? article = await articlesRepository.GetArticleByIdAsync(id, continent);
        if (article == null) return NotFound();

        return Ok(article);
    }

    [HttpPost("{continent}")]
    public async Task<IActionResult> CreateArticle(Article article, Continent continent)
    {
        Article? createdArticle = await articlesRepository.CreateArticleAsync(article, continent);
        return CreatedAtAction(
            nameof(GetArticle), 
            new { continent = continent.ToString(), id = createdArticle.Id}, 
            createdArticle);
    }

    [HttpPut("{continent}")]
    public async Task<IActionResult> UpdateArticle(Article updatedArticle, Continent continent)
    {
        Article? article = await articlesRepository.UpdateArticleAsync(updatedArticle, continent);
        if (article == null) return NotFound();

        return Ok(article);
    }

    [HttpDelete("{continent}/{id}")]
    public async Task<IActionResult> DeleteArticle(int id, Continent continent)
    {
        bool result = await articlesRepository.DeleteArticleAsync(id, continent);
        if (!result) return NotFound();

        return NoContent();
    }
}