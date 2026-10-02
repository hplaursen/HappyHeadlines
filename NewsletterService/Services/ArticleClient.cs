using Messages;

namespace NewsletterService.Services;

public class ArticleClient(HttpClient client)
{
    public async Task<ArticleDto?> GetDailyArticleAsync()
    {
        return await client.GetFromJsonAsync<ArticleDto>("/api/Article/Global/1");
    }   
}