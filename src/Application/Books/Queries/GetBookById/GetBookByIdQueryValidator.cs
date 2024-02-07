using clean_arc_api.Application.Books.Queries.GetBookById;

namespace clean_arc_api.Application.Books.Queries.GetBooks;

public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book Id is required.");
    }
}
