using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog.DataAccess;

public enum ContextType {SQLite, SQLServer}

public class ContextFactory: IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        return GetNewContext();
    }

    public static BlogDbContext GetNewContext(ContextType type = ContextType.SQLServer)
    {
        var builder = new DbContextOptionsBuilder<BlogDbContext>();
        DbContextOptions options = null;

        if (type == ContextType.SQLite)
        {
            options = GetSqliteConfig(builder);
        }
        else
        {
            options = GetSqlServerConfig(builder);
        }

        return new BlogDbContext(options);
    }

    private static DbContextOptions GetSqliteConfig(DbContextOptionsBuilder builder)
    {
        var connection = new SqliteConnection("Filename=:memory:");
        builder.UseSqlite(connection);
        return builder.Options;
    }

    private static DbContextOptions GetSqlServerConfig(DbContextOptionsBuilder builder)
    {
        var directory = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BlogDb");
        builder.UseSqlServer(connectionString);

        return builder.Options;
    }
}