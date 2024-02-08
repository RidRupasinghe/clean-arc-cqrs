using clean_arc_api.Application.Books.Queries.GetBooks;
using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Domain.Entities;

namespace clean_arc_api.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand(int Id) : IRequest<BookDto>
{
    public required string Title { get; init; }
    public required int CategoryId { get; init; }
    public string? ISBN { get; init; }
}

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");

        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.CategoryId)
            .NotEmpty()
            .Must(id =>
            {
                return context.BookCategories.Any(x => x.Id == id);
            })
            .WithErrorCode("NotExists")
            .WithMessage("Invalid category");
    }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, book);

        book.Title = request.Title;
        book.ISBN = request.ISBN;

        if (book.CategoryId != request.CategoryId)
        {
            BookCategory bookCategory = _context.BookCategories.FirstOrDefault(v => v.Id == request.CategoryId)!;
            book.BookCategory = bookCategory;
            book.CategoryId = bookCategory.Id;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BookDto>(book);
    }
}
