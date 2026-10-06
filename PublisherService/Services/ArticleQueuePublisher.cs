using System.Diagnostics;
using EasyNetQ;
using Messages;
using Monitoring;
namespace PublisherService.Services;

public class ArticleQueuePublisher(IBus bus, ILogger<ArticleQueuePublisher> logger)
{
    public async Task PublishAsync(ArticleDto article)
    {

        using Activity? activity =
            TracingService.ActivitySource.StartActivity("Publish Article");

        logger.LogInformation("Publishing article {Title}", article.Title);

        Dictionary<string, object> headers = [];

        if (activity != null)
            headers["traceparent"] = activity.Id!;

        await bus.PubSub.PublishAsync(article, config => config.WithHeaders(headers));
        //await bus.PubSub.PublishAsync<ArticleDto>(article);
    }
}