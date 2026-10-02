using StackExchange.Redis;
using System.Text.Json;

namespace ArticleService.Data;

public class ArticleCache(IConnectionMultiplexer redis)
{
    private readonly IDatabase redisDB = redis.GetDatabase();

    public async Task SetAsync<Article>(string key, Article value)
    {
        string json = JsonSerializer.Serialize(value);
        await redisDB.StringSetAsync(key, json);
    }

    public async Task<Article?> GetAsync<Article>(string key)
    {
        RedisValue value = await redisDB.StringGetAsync(key);

        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<Article>(value.ToString());
    }
}