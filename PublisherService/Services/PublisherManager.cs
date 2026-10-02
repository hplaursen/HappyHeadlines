using Messages;

namespace PublisherService.Services;


public class PublisherManager(ArticleQueuePublisher articleQueuePublisher)
{

    public async Task PublishArticleAsync(ArticleDto article)
    {
        await articleQueuePublisher.PublishAsync(article);
    }

}
