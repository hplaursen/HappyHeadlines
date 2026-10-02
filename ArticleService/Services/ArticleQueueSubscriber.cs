using EasyNetQ;
using Messages;

namespace ArticleService.Services;

public class ArticleQueueSubscriber(IBus bus) : BackgroundService
{
    private Task HandleArticleAsync(ArticleDto article)
    {
        Console.WriteLine($"ArticleService Received: {article.Content}");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await bus.PubSub.SubscribeAsync<ArticleDto>("latest-articles", HandleArticleAsync);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
        
        Console.WriteLine("ArticleSubscriber is stopping..");
    }
}