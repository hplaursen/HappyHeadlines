using ArticleService.Data;
using ArticleService.Models;
using ArticleService.Services;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Repositories;
public class ArticleRepository(DbRouter dbRouter, ArticleCache articleCache)
{

    public async Task<Article> CreateArticleAsync(Article article, Continent continent)
    {
        using ArticleContext? dbContext = dbRouter.GetDbContext(continent);

        dbContext.Articles.Add(article);
        await dbContext.SaveChangesAsync();
        return article;
    }

    public async Task<Article?> GetArticleByIdAsync(int id, Continent continent)
    {
        string key = $"article:{continent}:{id}";

        Article? cachedArticle = 
            await articleCache.GetAsync<Article>(key);

        if (cachedArticle != null)
            return cachedArticle;


        using ArticleContext dbContext = dbRouter.GetDbContext(continent);
        
        Article? article = await dbContext.Articles.FindAsync(id);

        if (article != null)
            await articleCache.SetAsync(key, article);

        return article;
    }

    public async Task<List<Article>> GetArticlesPublishedSinceAsync(Continent continent, DateTime fromDate)
    {
        using ArticleContext dbContext = dbRouter.GetDbContext(continent);

        return await dbContext.Articles
            .Where(x => x.PublishedAt >= fromDate).ToListAsync();
    }

    public async Task<Article?> UpdateArticleAsync(Article updatedArticle, Continent continent)
    {
        using ArticleContext dbContext = dbRouter.GetDbContext(continent);

        Article? article = await dbContext.Articles.FindAsync(updatedArticle.Id);
        if (article == null) return null;

        article.Title = updatedArticle.Title;
        article.Content = updatedArticle.Content;
        article.PublishedAt = updatedArticle.PublishedAt;

        await dbContext.SaveChangesAsync();
        return article;
    }

    public async Task<bool> DeleteArticleAsync(int id, Continent continent)
    {
        using ArticleContext dbContext = dbRouter.GetDbContext(continent);

        Article? article = await dbContext.Articles.FindAsync(id);
        if (article == null) return false;

        dbContext.Articles.Remove(article);
        await dbContext.SaveChangesAsync();
        return true;
    }   

}


