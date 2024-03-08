using Domain.Events;

namespace Domain.Entities;

public class Snippet : BaseAuditableEntity    
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public string Code { get; set; }
    public string Docs { get; set; }

    private bool _isPinned;
    public bool IsPinned
    {
        get => _isPinned;
        set
        {
            if (value && !_isPinned)
            {
                AddDomainEvent(new SnippetPinnedEvent(this));
            }

            _isPinned = value;
        }
    }

    public ICollection<Tag> Tags { get; set; } = [];
}
