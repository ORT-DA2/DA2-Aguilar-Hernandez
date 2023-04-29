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

    public BlogDbContext(DbContextOptions options): base(options)
    {
    }
    
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
            .HasOne(u => u.Owner)
            .WithMany(c => c.Comments)
            .OnDelete(DeleteBehavior.NoAction);
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

            var connectionString = configuration.GetConnectionString("BlogDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    
}