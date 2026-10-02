using EasyNetQ;
using Messages;

namespace PublisherService.Services;

public class ArticleQueuePublisher(IBus bus)
{
    public async Task PublishAsync(ArticleDto article)
    {
        await bus.PubSub.PublishAsync<ArticleDto>(article);
    }

}