using clean_arc_api.Application.Common.Interfaces;
using clean_arc_api.Application.Common.Mappings;
using clean_arc_api.Application.Common.Models;

namespace clean_arc_api.Application.Books.Queries.GetBooks;

public record GetBooksQuery : IRequest<PaginatedList<BookDto>>
{
    public int CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedList<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Where(x => x.CategoryId == request.CategoryId)
            .OrderBy(x => x.Id)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
