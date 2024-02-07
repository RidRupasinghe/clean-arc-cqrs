using clean_arc_api.Application.Common.Exceptions;
using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Domain.Entities;
using clean_arc_api.Domain.Events;
using clean_arc_api.Domain.Exceptions;

namespace clean_arc_api.Application.Books.Commands.CreateBook;

public record CreateBookCommand : IRequest<int>
{
    public required string Title { get; init; }
    public required int CategoryId { get; init; }
    public string? ISBN { get; init; }
}

// public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
// {
//     public CreateBookCommandValidator()
//     {
//         RuleFor(v => v.Title)
//             .MaximumLength(200)
//             .NotEmpty();
//         
//         RuleFor(v => v.CategoryId)
//             .NotEmpty()
//             .Must(id =>
//             {
//                 using (var db = HostContext.AppHost.GetDbConnection(base.Request))
//                 {
//                     return !_context.Exists<EntityName>(x => x.Id == id);
//                 }
//             })
//             .WithErrorCode("AlreadyExists")
//             .WithMessage("...");
//     }
// }

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

        // if (bookCategory == null)
        // {
        //     throw new EntityNotFoundException(typeof(BookCategory).ToString(), request.CategoryId);
        // }

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
