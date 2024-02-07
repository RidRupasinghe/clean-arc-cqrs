using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Domain.Entities;
using clean_arc_api.Domain.Events;

namespace clean_arc_api.Application.BookCategories.Commands.CreateBookCategory;

public record CreateBookCategoryCommand : IRequest<int>
{
    public required string Title { get; init; }
}

public class CreateBookCategoryCommandValidator : AbstractValidator<CreateBookCategoryCommand>
{
    public CreateBookCategoryCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}

public class CreateBookCategoryCommandHandler : IRequestHandler<CreateBookCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBookCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBookCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new BookCategory { Title = request.Title };

        entity.AddDomainEvent(new BookCategoryCreatedEvent(entity));

        _context.BookCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
