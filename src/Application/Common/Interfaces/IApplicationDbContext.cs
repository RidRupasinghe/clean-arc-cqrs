using clean_arc_api.Domain.Entities;

namespace clean_arc_api.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Book> Books { get; }

    DbSet<BookCategory> BookCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
