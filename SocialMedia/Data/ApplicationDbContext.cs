using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts);
        RestrictCascadeDeletion(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void RestrictCascadeDeletion(ModelBuilder builder)
    {
        var tableRelationKeys = builder.Model
            .GetEntityTypes()
            .SelectMany(entity => entity.GetForeignKeys());

        foreach (var relationship in tableRelationKeys)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

}