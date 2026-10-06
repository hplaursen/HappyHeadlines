using System.Diagnostics;
using EasyNetQ;
using Messages;
using Monitoring;
using System.Text;

namespace ArticleService.Services;

public class ArticleQueueSubscriber(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await using var consumer =
            await bus.Advanced.ConsumeAsync(config =>
            {
                config.ForQueue<ArticleDto>(
                    new EasyNetQ.Topology.Queue(
                        "Messages.ArticleDto, Messages_latest-articles",
                        false),
                    async (message, info, cancellationToken) =>
                    {

string? traceparent = null;

if (message.Properties.Headers?.TryGetValue(
        "traceparent",
        out var header) == true)
{
    traceparent = header switch
    {
        byte[] bytes => Encoding.UTF8.GetString(bytes),
        string value => value,
        _ => header?.ToString()
    };
}

                            Console.WriteLine(
    $"traceparent received: {traceparent}");

    Console.WriteLine(
    $"traceparent received: {traceparent}");

                        ActivityContext.TryParse(
                            traceparent,
                            null,
                            out var parentContext);

                        using Activity? activity =
                            TracingService.ActivitySource.StartActivity(
                                "Receive Article",
                                ActivityKind.Consumer,
                                parentContext);

Console.WriteLine(
    $"Receive Article Activity: {(activity == null ? "NULL" : activity.Id)}");
                        Console.WriteLine(
                            $"ArticleService Received: {message.Body.Content}");

                        await Task.CompletedTask;
                    });
            });

        try
        {
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
        catch (OperationCanceledException)
        {
        }
    }
}