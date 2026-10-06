using DraftService.Models;
using Microsoft.EntityFrameworkCore;
using Resilience;

namespace DraftService.Data;


public static class PrepDB
{
public static async Task MigrateAndSeedData(
    IServiceProvider services)
{
    using IServiceScope scope = services.CreateScope();

    DraftContext dbContext =
        scope.ServiceProvider.GetRequiredService<DraftContext>();

    await DatabaseRetry.ExecuteAsync(async cancellationToken =>
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    });

    SeedData(dbContext);
}

    private static void SeedData(DraftContext dbContext)
    {
        dbContext.AddRange(
            new Draft
            {
                Title = "More to Explore",
                Content = "I would like to see more about this.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "A Clear Explanation",
                Content = "This makes a lot of sense.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },

            new Draft
            {
                Title = "Useful Information",
                Content = "Thanks for the information.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "Worth Discussing",
                Content = "This is something worth discussing.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "Good Explanation",
                Content = "Good explanation of the topic.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },

            new Draft
            {
                Title = "A Strong Point",
                Content = "I completely agree with this.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "Interesting Points",
                Content = "Interesting points in this article.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "More Information",
                Content = "I would like to know more about this.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },

            new Draft
            {
                Title = "A Good Read",
                Content = "This was a surprisingly good read.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "Interesting Questions",
                Content = "I think this raises some interesting questions.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            },
            new Draft
            {
                Title = "Looking Forward",
                Content = "Looking forward to seeing more on this topic.",
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow
            }
        );
        dbContext.SaveChanges();
    }

}