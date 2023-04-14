﻿using System.Data.Common;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.DataAccess;

public class BlogDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }

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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new {ur.UserId, ur.Role});

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .Property(ur => ur.Role)
            .HasConversion<string>();
    }
}