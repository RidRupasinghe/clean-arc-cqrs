using clean_arc_api.Application.Books.Commands.CreateBook;
using clean_arc_api.Application.Books.Commands.DeleteBook;
using clean_arc_api.Application.Books.Commands.UpdateBook;
using clean_arc_api.Application.Books.Queries.GetBookById;
using clean_arc_api.Application.Books.Queries.GetBooks;
using clean_arc_api.Application.Common.Models;

namespace clean_arc_api.Web.Endpoints;

public class Books : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetBooks)
            .MapPost(CreateBook)
            .MapGet(GetBookById, "{id}")
            .MapPut(UpdateBook, "{id}")
            .MapDelete(DeleteBook, "{id}")
            ;
    }

    public async Task<PaginatedList<BookDto>> GetBooks(ISender sender, [AsParameters] GetBooksQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<BookDto> GetBookById(ISender sender, [AsParameters] GetBookByIdQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateBook(ISender sender, CreateBookCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<BookDto> UpdateBook(ISender sender, UpdateBookCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<bool> DeleteBook(ISender sender, int id)
    {
        return await sender.Send(new DeleteBookCommand(id));
    }
}
