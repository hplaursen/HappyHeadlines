using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriberService.Data;
using SubscriberService.Models;
using SubscriberService.Services;

namespace SubscriberService.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SubscriberController(SubscriberContext database, ReleaseToggle toggle, SubscriberQueuePublisher queue) : ControllerBase
{
    [HttpGet("status")]
    public async Task<IActionResult> Status(CancellationToken cancellationToken) =>
        Ok(new { enabled = await toggle.IsEnabledAsync(cancellationToken) });

    // Runtime release control. Connect this endpoint to the deployment's admin network/access policy.
    [HttpPut("status")]
    public async Task<IActionResult> SetStatus([FromBody] ToggleRequest request, CancellationToken cancellationToken)
    {
        await toggle.SetEnabledAsync(request.Enabled, cancellationToken);
        return Ok(new { enabled = await toggle.IsEnabledAsync(cancellationToken) });
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest request, CancellationToken cancellationToken)
    {
        if (!await toggle.IsEnabledAsync(cancellationToken)) return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "SubscriberService is disabled." });
        var email = request.Email.Trim().ToLowerInvariant();
        var existing = await database.Subscribers.SingleOrDefaultAsync(item => item.Email == email, cancellationToken);
        if (existing is not null) return Conflict(new { error = "This email is already subscribed." });

        var subscriber = new Subscriber { Email = email };
        database.Subscribers.Add(subscriber);
        await database.SaveChangesAsync(cancellationToken);
        await queue.PublishWelcomeAsync(new SubscriberWelcomeMessage(subscriber.Id, subscriber.Email, subscriber.CreatedAt), cancellationToken);
        return Created($"/api/subscriber/{subscriber.Id}", new { subscriber.Id, subscriber.Email, subscriber.CreatedAt });
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> Unsubscribe(string email, CancellationToken cancellationToken)
    {
        if (!await toggle.IsEnabledAsync(cancellationToken)) return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "SubscriberService is disabled." });
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var subscriber = await database.Subscribers.SingleOrDefaultAsync(item => item.Email == normalizedEmail, cancellationToken);
        if (subscriber is null) return NotFound();
        database.Subscribers.Remove(subscriber);
        await database.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        if (!await toggle.IsEnabledAsync(cancellationToken)) return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "SubscriberService is disabled." });
        var subscribers = await database.Subscribers.AsNoTracking().OrderBy(item => item.CreatedAt).ToListAsync(cancellationToken);
        return Ok(subscribers);
    }

    public sealed record SubscribeRequest(string Email);
    public sealed record ToggleRequest(bool Enabled);
}
