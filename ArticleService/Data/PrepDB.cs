using ArticleService.Models;
using ArticleService.Services;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Data;


public static class PrepDB
{
    public static void MigrateAndSeedData(IServiceProvider services)
    {
        using IServiceScope? scope = services.CreateScope();

        DbRouter? dbRouter = 
            scope.ServiceProvider.GetRequiredService<DbRouter>();

        foreach (Continent continent in Enum.GetValues<Continent>())
        {
            ArticleContext dbContext = dbRouter.GetDbContext(continent);

            dbContext.Database.Migrate();
            SeedData(dbContext, continent);
        }
    }

    private static void SeedData(ArticleContext dbContext, Continent continent)
    {
        dbContext.AddRange(
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent },
            new Article() { Title = "Lego",  Content = "fasd", Continent = continent }
        );
        dbContext.SaveChanges();
    }

}