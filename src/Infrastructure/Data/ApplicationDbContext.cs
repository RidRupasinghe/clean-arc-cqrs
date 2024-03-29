﻿using System.Reflection;
using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Domain.Entities;
using clean_arc_api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clean_arc_api.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<BookCategory> BookCategories => Set<BookCategory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>()
            .HasOne(c => c.BookCategory)
            .WithMany(p => p.Books)
            .HasForeignKey(c => c.CategoryId);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
