namespace Messages;

public sealed record SubscriberWelcomeMessage(Guid SubscriberId, string Email, DateTime SubscribedAt);
