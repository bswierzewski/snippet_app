using Domain.Entities;

namespace Domain.Events;

public class SnippetDeletedEvent : BaseEvent
{
    public SnippetDeletedEvent(Snippet snippet)
    {
        Snippet = snippet;
    }

    public Snippet Snippet { get; }
}
