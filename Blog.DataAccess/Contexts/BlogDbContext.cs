using System.Data.Common;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.DataAccess;

public class BlogDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    
    public BlogDbContext(DbContextOptions options): base(options)
    {
    }
    
    public BlogDbContext() : base() {}

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