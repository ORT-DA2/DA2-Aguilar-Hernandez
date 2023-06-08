using Blog.Domain;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.DataAccess;

public class BlogDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Notification> Notifications {get; set; }
    public DbSet<OffensiveWord> OffensiveWords { get; set; }
    public BlogDbContext(DbContextOptions options): base(options){}

    public BlogDbContext() : base() {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new {ur.UserId, ur.Role});

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .Property(ur => ur.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Comments)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Notification>()
            .HasOne(n=>n.Comment)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Notification>()
            .HasOne(n=>n.Article)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Notification>()
            .HasOne(n=>n.UserToNotify)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OffensiveWord>()
            .HasMany(o => o.Articles)
            .WithMany(a => a.OffensiveContent)
            .UsingEntity(j => j.ToTable("ArticleOffensiveWord"));

        modelBuilder.Entity<OffensiveWord>()
            .HasMany(o => o.Comments)
            .WithMany(c => c.OffensiveContent)
            .UsingEntity(j => j.ToTable("CommentOffensiveWord"));

    }
    
    public override int SaveChanges()
    {
        var deletedUsers = ChangeTracker.Entries<User>()
            .Where(e => e.State == EntityState.Deleted)
            .Select(e => e.Entity)
            .ToList();
        var deletedComments = ChangeTracker.Entries<Comment>()
            .Where(e => e.State == EntityState.Deleted)
            .Select(e => e.Entity)
            .ToList();
        var deletedArticles = ChangeTracker.Entries<Article>()
            .Where(e => e.State == EntityState.Deleted)
            .Select(e => e.Entity)
            .ToList();

        foreach (var childrenToDelete in deletedUsers.Select(parent => Articles.Where(c => c.Owner.Id == parent.Id).ToList()))
        {
            Articles.RemoveRange(childrenToDelete);
            foreach (var commentsToDelete in childrenToDelete.Select(parent => Comments.Where(c => c.Article.Id == parent.Id).ToList()))
            {
                Comments.RemoveRange(commentsToDelete);
                foreach (var commentsNotificationsToDelete in commentsToDelete.Select(parent => Notifications.Where(c => c.Comment.Id == parent.Id).ToList()))
                {
                    Notifications.RemoveRange(commentsNotificationsToDelete);
                }
            }
            foreach (var articlesNotificationsToDelete in childrenToDelete.Select(parent => Notifications.Where(c => c.Article.Id == parent.Id).ToList()))
            {
                Notifications.RemoveRange(articlesNotificationsToDelete);
            }
            
        }
        
        foreach (var childrenToDelete in deletedComments.Select(parent => Notifications.Where(c => c.Comment.Id == parent.Id).ToList()))
        {
            Notifications.RemoveRange(childrenToDelete);
        }
        
        foreach (var childrenToDelete in deletedArticles.Select(parent => Notifications.Where(c => c.Article.Id == parent.Id).ToList()))
        {
            Notifications.RemoveRange(childrenToDelete);
        }
        

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();
        
            var connectionString = configuration.GetConnectionString("BlogDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    
}