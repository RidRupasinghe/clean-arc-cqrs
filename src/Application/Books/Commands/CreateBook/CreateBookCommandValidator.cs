using clean_arc_api.Application.Common.Interfaces;

namespace clean_arc_api.Application.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator(IApplicationDbContext context)
    {
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
