using anonim_chat.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace anonim_chat.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserName)
                .IsUnique();

            entity.Property(e => e.UserName)
                .HasMaxLength(50);
        });


        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(e => e.Messages)
             .WithOne(e => e.Chat)
             .HasForeignKey(e => e.ChatId)
             .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ChatId, e.UserId });
            entity.HasOne(e => e.Chat)
             .WithMany(e => e.Messages)
             .HasForeignKey(e => e.ChatId)
             .OnDelete(DeleteBehavior.NoAction);
        });
    }
}