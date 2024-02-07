using clean_arc_api.Application.BookCategories.Commands.CreateBookCategory;

namespace clean_arc_api.Web.Endpoints;

public class BookCategories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            // .MapGet(GetBookCategoriesWithPagination)
            .MapPost(CreateBookCategory)
            // .MapPut(UpdateBookCategory, "{id}")
            // .MapDelete(DeleteBookCategory, "{id}")
            ;
    }

    public async Task<int> CreateBookCategory(ISender sender, CreateBookCategoryCommand command)
    {
        return await sender.Send(command);
    }
}
