using ArticleService.Data;
using ArticleService.Models;
using ArticleService.Repositories;
using StackExchange.Redis;

namespace ArticleService.Services;

public class ArticleCacheRefreshService(IServiceScopeFactory scopeFactory, IConnectionMultiplexer redis) 
: BackgroundService
{
    private readonly IDatabase cache = redis.GetDatabase();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await RefreshCacheAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing article cache: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMinutes(3),stoppingToken);
        }
    }

    private async Task RefreshCacheAsync(
        CancellationToken cancellationToken)
    {
        using IServiceScope scope = scopeFactory.CreateScope();

        ArticleRepository repository =
            scope.ServiceProvider
                .GetRequiredService<ArticleRepository>();

        DateTime fromDate = DateTime.UtcNow.AddDays(-14);

        // Vi henter artikler fra alle kontinenter
        foreach (Continent continent in Enum.GetValues<Continent>())
        {
            List<Article> articles =
                await repository.GetArticlesPublishedSinceAsync(continent, fromDate);

            foreach (Article article in articles)
            {
                string key = $"article:{continent}:{article.Id}";
                string json = System.Text.Json.JsonSerializer.Serialize(article);

                await cache.StringSetAsync(key, json);
            }
        }
    }
}