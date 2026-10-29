using Microsoft.EntityFrameworkCore;
using SubscriberService.Models;

namespace SubscriberService.Data;

public sealed class SubscriberContext(DbContextOptions<SubscriberContext> options) : DbContext(options)
{
    public DbSet<Subscriber> Subscribers => Set<Subscriber>();
    public DbSet<SubscriberFeatureState> FeatureStates => Set<SubscriberFeatureState>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(subscriber => subscriber.Id);
            entity.Property(subscriber => subscriber.Email).HasMaxLength(320).IsRequired();
            entity.HasIndex(subscriber => subscriber.Email).IsUnique();
        });
    }
}
