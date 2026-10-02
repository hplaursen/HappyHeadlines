using CommentService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Data
{
    public class CommentContext(DbContextOptions<CommentContext> options) 
    : DbContext(options)
    {
        public DbSet<Comment> Comments { get; set; }
    }
}