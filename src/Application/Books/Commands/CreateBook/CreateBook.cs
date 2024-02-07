using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Domain.Entities;
using clean_arc_api.Domain.Events;

namespace clean_arc_api.Application.Books.Commands.CreateBook;

public record CreateBookCommand : IRequest<int>
{
    public required string Title { get; init; }
    public required int CategoryId { get; init; }
    public string? ISBN { get; init; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        BookCategory bookCategory = _context.BookCategories.FirstOrDefault(v => v.Id == request.CategoryId)!;

        var entity = new Book
        {
            CategoryId = request.CategoryId, Title = request.Title, ISBN = request.ISBN, BookCategory = bookCategory
        };

        entity.AddDomainEvent(new BookCreatedEvent(entity));

        _context.Books.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
