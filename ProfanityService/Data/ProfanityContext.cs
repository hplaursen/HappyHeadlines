using ProfanityService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfanityService.Data
{
    public class ProfanityContext(DbContextOptions<ProfanityContext> options) 
    : DbContext(options)
    {
        public DbSet<Profanity> Profanities { get; set; }
    }
}