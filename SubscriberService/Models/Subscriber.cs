namespace SubscriberService.Models;

public sealed class Subscriber
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
