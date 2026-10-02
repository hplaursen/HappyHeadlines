using ArticleService.Data;
using ArticleService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Services;


public class DbRouter(IConfiguration config)
{
    public ArticleContext GetDbContext(Continent continent)
    {
        string connectionString = GetConnectionString(continent);

        DbContextOptions<ArticleContext> options = 
            new DbContextOptionsBuilder<ArticleContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new ArticleContext(options);
    }


    public string GetConnectionString(Continent continent)
    {
        IConfigurationSection dbConfig = 
            config.GetSection($"Database:Databases:{continent.ToString()}");

        string host = dbConfig["Host"]!;
        string port = dbConfig["Port"]!;
        string name = config["Database:Name"]!;
        string user = config["Database:User"]!;
        string password = config["Database:Password"]!;
        
        return $"Host={host};Port={port};Database={name};User Id={user};Password={password}";

    }
}