using clean_arc_api.Application.Books.Commands.CreateBook;

namespace clean_arc_api.Web.Endpoints;

public class Books : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            // .MapGet(GetBookCategoriesWithPagination)
            .MapPost(CreateBook)
            // .MapPut(UpdateBookCategory, "{id}")
            // .MapDelete(DeleteBookCategory, "{id}")
            ;
    }

    public async Task<int> CreateBook(ISender sender, CreateBookCommand command)
    {
        return await sender.Send(command);
    }
}
