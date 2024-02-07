namespace clean_arc_api.Domain.Events;

public class BookCategoryCreatedEvent : BaseEvent
{
    public BookCategoryCreatedEvent(BookCategory item)
    {
        Item = item;
    }

    public BookCategory Item { get; }
}
