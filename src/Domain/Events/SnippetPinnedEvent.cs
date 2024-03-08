using Domain.Entities;

namespace Domain.Events;

public class SnippetPinnedEvent : BaseEvent
{
    public SnippetPinnedEvent(Snippet snippet)
    {
        Snippet = snippet;
    }

    public Snippet Snippet { get; }
}
