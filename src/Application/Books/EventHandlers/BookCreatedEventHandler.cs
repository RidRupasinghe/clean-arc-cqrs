using clean_arc_api.Domain.Events;
using Microsoft.Extensions.Logging;

namespace clean_arc_api.Application.Books.EventHandlers;

public class BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
{
    private readonly ILogger<BookCreatedEventHandler> _logger;

    public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("clean_arc_api Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
