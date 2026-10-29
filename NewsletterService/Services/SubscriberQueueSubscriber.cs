using EasyNetQ;
using Messages;

namespace NewsletterService.Services;

/// <summary>Consumes welcome events independently so a subscriber broker outage does not stop newsletter APIs.</summary>
public sealed class SubscriberQueueSubscriber(IConfiguration configuration, ILogger<SubscriberQueueSubscriber> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionString = configuration.GetConnectionString("subscriberQueue");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            logger.LogWarning("Subscriber queue is not configured; welcome messages will not be consumed.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var services = new ServiceCollection();
                services.AddEasyNetQ(connectionString);
                using var provider = services.BuildServiceProvider();
                var bus = provider.GetRequiredService<IBus>();
                await bus.PubSub.SubscribeAsync<SubscriberWelcomeMessage>(
                    "newsletter-welcome",
                    message =>
                    {
                        logger.LogInformation("Welcome email requested for subscriber {SubscriberId} ({Email})", message.SubscriberId, message.Email);
                        return Task.CompletedTask;
                    });
                logger.LogInformation("Connected to subscriber queue.");
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogWarning(exception, "Subscriber queue is unavailable; retrying in 10 seconds. NewsletterService remains available.");
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
