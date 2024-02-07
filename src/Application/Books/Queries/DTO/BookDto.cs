using clean_arc_api.Domain.Entities;

namespace clean_arc_api.Application.Books.Queries.GetBooks;

public class BookDto
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public int CategoryId { get; init; }
    public string? Category { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.BookCategory!.Title));
        }
    }
}
