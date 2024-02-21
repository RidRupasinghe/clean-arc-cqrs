using clean_arc_api.Application.Books.Queries.GetBooks;
using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Application.Common.Security;
using clean_arc_api.Domain.Constants;

namespace clean_arc_api.Application.Books.Queries.GetBookById;

[Authorize(Roles = $"{Roles.Administrator}, {Roles.Customer}")]
public record GetBookByIdQuery(int Id) : IRequest<BookDto>
{
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.BookCategory)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, book);
        return _mapper.Map<BookDto>(book);
    }
}
