using Domain.Entities;

namespace Domain.Events;

public class SnippetCreatedEvent : BaseEvent
{
    public SnippetCreatedEvent(Snippet snippet)
    {
        Snippet = snippet;
    }

    public Snippet Snippet { get; }
}
