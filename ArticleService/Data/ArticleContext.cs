using ArticleService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Data
{
    public class ArticleContext(DbContextOptions<ArticleContext> options) 
    : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
    }
}