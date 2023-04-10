using System.Data.Common;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions options): base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}