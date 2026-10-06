using CommentService.Models;
using StackExchange.Redis;
using System.Text.Json;
using Prometheus;


namespace CommentService.Data;
public class CommentCache(IConnectionMultiplexer redis)
{
    private readonly IDatabase cache = redis.GetDatabase();
    private readonly string lruKey = "comment-cache-lru";
    private readonly string commentKey = "comments:article:";

    private static readonly Counter CacheHits =
        Metrics.CreateCounter(
            "comment_cache_hits_total",
            "Number of comment cache hits.");

    private static readonly Counter CacheMisses =
        Metrics.CreateCounter(
            "comment_cache_misses_total",
            "Number of comment cache misses.");

    public async Task<List<Comment>?> GetAsync(int articleId)
    {
        string? cacheComments =
            await cache.StringGetAsync(commentKey + articleId);

        if (cacheComments == null)
        {
            CacheMisses.Inc();
            return null;
        }

        CacheHits.Inc();

        await UpdateLruAsync(articleId);

        return JsonSerializer.Deserialize<List<Comment>>(cacheComments);
    }

    public async Task SetAsync(int articleId, List<Comment> comments)
    {
        long count = await cache.SortedSetLengthAsync(lruKey);

        if (count >= 3)
            await RemoveLeastRecentlyUsedAsync();

        string cacheComments = JsonSerializer.Serialize(comments);

        await cache.StringSetAsync(commentKey+articleId, cacheComments);
        await UpdateLruAsync(articleId);
    }

    private async Task UpdateLruAsync(int articleId)
    {
        double timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await cache.SortedSetAddAsync(lruKey, articleId, timestamp);
    }

    private async Task RemoveLeastRecentlyUsedAsync()
    {
        int? articleId = await GetLeastRecentlyUsedAsync();

        if (articleId == null)
            return;

        await cache.KeyDeleteAsync(commentKey + articleId);
        await cache.SortedSetRemoveAsync(lruKey, articleId);
    }

    private async Task<int?> GetLeastRecentlyUsedAsync()
    {
        RedisValue[] result = await cache.SortedSetRangeByRankAsync(lruKey, 0,0);

        if (result.Length == 0)
            return null;

        return (int)result[0];
    }
}
