using CommentService.Models;
using Microsoft.EntityFrameworkCore;
using Resilience;

namespace CommentService.Data;


public static class PrepDB
{
public static async Task MigrateAndSeedData(
    IServiceProvider services)
{
    using IServiceScope scope = services.CreateScope();

    CommentContext dbContext =
        scope.ServiceProvider.GetRequiredService<CommentContext>();

    await DatabaseRetry.ExecuteAsync(async cancellationToken =>
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
    });

    SeedData(dbContext);
}

    private static void SeedData(CommentContext dbContext)
    {
        dbContext.AddRange(
            // Article 1
            new Comment { ArticleId = 1, Content = "Really interesting article!", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 1, Content = "I agree with the points made here.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 1, Content = "This was a very useful read.", CreatedAt = DateTime.UtcNow },

            // Article 2
            new Comment { ArticleId = 2, Content = "I had not considered this before.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 2, Content = "Great article, thanks for sharing.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 2, Content = "There are some good points here.", CreatedAt = DateTime.UtcNow },

            // Article 3
            new Comment { ArticleId = 3, Content = "Very interesting perspective.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 3, Content = "I would like to see more about this.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 3, Content = "This makes a lot of sense.", CreatedAt = DateTime.UtcNow },

            // Article 4
            new Comment { ArticleId = 4, Content = "Thanks for the information.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 4, Content = "This is something worth discussing.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 4, Content = "Good explanation of the topic.", CreatedAt = DateTime.UtcNow },

            // Article 5
            new Comment { ArticleId = 5, Content = "I completely agree with this.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 5, Content = "Interesting points in this article.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 5, Content = "I would like to know more about this.", CreatedAt = DateTime.UtcNow },

            // Article 6
            new Comment { ArticleId = 6, Content = "This was a surprisingly good read.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 6, Content = "I think this raises some interesting questions.", CreatedAt = DateTime.UtcNow },
            new Comment { ArticleId = 6, Content = "Looking forward to seeing more on this topic.", CreatedAt = DateTime.UtcNow }
        );
        dbContext.SaveChanges();
    }

}