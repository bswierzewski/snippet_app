using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Events;

namespace Application.Snippets.Commands.CreateSnippet;

public record CreateSnippetCommand() : IRequest<int>
{    
    public string Title { get; init; }
    public string Description { get; init; }
    public string Language { get; init; }
    public string Code { get; init; }
    public string Docs { get; init; }
    public string[] Tags { get; init; } = [];
    public string[] ImageUrls { get; init; } = [];
}

public class CreateSnippetCommandHandler : IRequestHandler<CreateSnippetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateSnippetCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSnippetCommand request, CancellationToken cancellationToken)
    {
        var tags = request.Tags.Select(tag => tag.ToLower());

        // Remove duplicates and find existing tags in the database
        var tagsFromDb = _context.Tags
            .Where(tag => tags.Contains(tag.Name))
            .ToList();

        // Add new tags that don't exist in the database
        var newTags = tags
            .Where(tag => !tagsFromDb.Any(dbTag => dbTag.Name == tag))
            .Select(tag => new Tag { Name = tag })
            .ToList();

        // Combine existing and new tags without duplicates
        var allTags = tagsFromDb.Concat(newTags).ToList();

        var entity = new Snippet
        {
            Tags = allTags
        };

        _mapper.Map(request, entity);

        entity.AddDomainEvent(new SnippetCreatedEvent(entity));

        _context.Snippets.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
