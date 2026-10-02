using DraftService.Models;
using Microsoft.EntityFrameworkCore;

namespace DraftService.Data
{
    public class DraftContext(DbContextOptions<DraftContext> options) 
    : DbContext(options)
    {
        public DbSet<Draft> Drafts { get; set; }
    }
}