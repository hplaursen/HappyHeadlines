using Messages;
namespace NewsletterService.Services;


public class NewsletterManager(ArticleClient articleClient)
{

    public async Task<ArticleDto?> GetDailyArticleAsync()
    {
        ArticleDto? response = 
            await articleClient.GetDailyArticleAsync();
        if(response == null) return null;
        return response;
    }

}
