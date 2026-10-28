using ArticleService.Models;
using ArticleService.Services;
using Microsoft.EntityFrameworkCore;
using Resilience;

namespace ArticleService.Data;


public static class PrepDB
{
    public static async Task MigrateAndSeedData(IServiceProvider services)
    {
        using IServiceScope? scope = services.CreateScope();

        DbRouter? dbRouter = 
            scope.ServiceProvider.GetRequiredService<DbRouter>();

        foreach (Continent continent in Enum.GetValues<Continent>())
        {
            ArticleContext dbContext = dbRouter.GetDbContext(continent);

            await DatabaseRetry.ExecuteAsync(async cancellationToken =>
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
            });
            
            SeedData(dbContext, continent);
        }
    }

    private static void SeedData(ArticleContext dbContext, Continent continent)
    {
        dbContext.AddRange(
            new Article
            {
                Title = "New AI Model Released",
                Content = "A new artificial intelligence model has been released with improved reasoning and coding capabilities.",
                Continent = continent,
                PublishedAt = DateTime.UtcNow
            },
            new Article
            {
                Title = "Global Markets Rise",
                Content = "Stock markets around the world showed positive movements following new economic data.",
                Continent = continent,
                PublishedAt = DateTime.UtcNow
            },
            new Article
            {
                Title = "Major Climate Report Published",
                Content = "A new climate report examines recent changes in global temperatures and weather patterns.",
                Continent = continent,
                PublishedAt = DateTime.UtcNow
            },
            new Article
            {
                Title = "New Smartphone Technology",
                Content = "Technology companies are introducing new smartphone features focused on battery life and performance.",
                Continent = continent,
                PublishedAt = DateTime.UtcNow
            },
            new Article
            {
                Title = "European Football Season Begins",
                Content = "Football clubs across Europe are preparing for the start of the new season.",
                Continent = continent
            },
            new Article
            {
                Title = "Scientists Discover New Species",
                Content = "Researchers have identified a previously unknown species during an expedition in a remote region.",
                Continent = continent
            },
            new Article
            {
                Title = "Electric Cars Become More Popular",
                Content = "Sales of electric vehicles continue to increase as manufacturers introduce new models.",
                Continent = continent
            },
            new Article
            {
                Title = "Major Cybersecurity Update",
                Content = "Security researchers have identified new vulnerabilities and recommend updating affected systems.",
                Continent = continent
            },
            new Article
            {
                Title = "New Space Mission Announced",
                Content = "A new space mission has been announced with the goal of studying distant objects in our solar system.",
                Continent = continent
            },
            new Article
            {
                Title = "Local Economy Shows Growth",
                Content = "New economic figures indicate continued growth across several important industries.",
                Continent = continent
            },
            new Article
            {
                Title = "New Programming Language Features",
                Content = "The latest programming language release introduces improvements to performance and developer productivity.",
                Continent = continent
            },
            new Article
            {
                Title = "Renewable Energy Production Increases",
                Content = "Renewable energy sources are producing a growing share of electricity in several countries.",
                Continent = continent
            }
        );
        dbContext.SaveChanges();
    }

}