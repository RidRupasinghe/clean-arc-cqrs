namespace clean_arc_api.Application.Books.Queries.GetBooks;

public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
{
    public GetBooksQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("ListId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
