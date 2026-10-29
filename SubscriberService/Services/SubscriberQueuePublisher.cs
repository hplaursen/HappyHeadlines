using EasyNetQ;
using Messages;

namespace SubscriberService.Services;

public sealed class SubscriberQueuePublisher(IBus bus)
{
    public Task PublishWelcomeAsync(SubscriberWelcomeMessage message, CancellationToken cancellationToken) =>
        bus.PubSub.PublishAsync(message, cancellationToken: cancellationToken);
}
