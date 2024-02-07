namespace clean_arc_api.Domain.Entities;

public class BookCategory : BaseAuditableEntity
{
    public required string Title { get; set; }

    public IList<Book> Books { get; private set; } = new List<Book>();
}
