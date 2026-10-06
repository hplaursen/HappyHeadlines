using Prometheus;
using StackExchange.Redis;
using System.Text.Json;

namespace ArticleService.Data;

public class ArticleCache(IConnectionMultiplexer redis)
{
    private readonly IDatabase redisDB = redis.GetDatabase();

    private static readonly Counter CacheHits =
        Metrics.CreateCounter(
            "article_cache_hits_total",
            "Number of article cache hits.");

    private static readonly Counter CacheMisses =
        Metrics.CreateCounter(
            "article_cache_misses_total",
            "Number of article cache misses.");

    public async Task SetAsync<Article>(string key, Article value)
    {
        string json = JsonSerializer.Serialize(value);
        await redisDB.StringSetAsync(key, json);
    }

    public async Task<Article?> GetAsync<Article>(string key)
    {
        RedisValue value = await redisDB.StringGetAsync(key);

        if (!value.HasValue)
        {
            CacheMisses.Inc();
            return default;
        }

        CacheHits.Inc();

        return JsonSerializer.Deserialize<Article>(value.ToString());
    }
}