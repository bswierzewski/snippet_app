using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Snippets.EventHandlers;

public class SnippetCreatedEventHandler : INotificationHandler<SnippetCreatedEvent>
{
    private readonly ILogger<SnippetCreatedEvent> _logger;

    public SnippetCreatedEventHandler(ILogger<SnippetCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(SnippetCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
