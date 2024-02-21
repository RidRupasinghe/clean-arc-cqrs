using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Application.Common.Security;
using clean_arc_api.Domain.Constants;

namespace clean_arc_api.Application.Books.Commands.DeleteBook;

[Authorize(Roles = $"{Roles.Administrator}")]
public record DeleteBookCommand(int Id) : IRequest<bool>
{
}

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");
    }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, book);

        _context.Books.Remove(book);

        // book.AddDomainEvent(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
