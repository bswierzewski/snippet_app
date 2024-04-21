using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Snippets.Commands.UpdateSnippet;

public record UpdateSnippetCommand : IRequest<int>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Language { get; init; }
    public string Code { get; init; }
    public string Docs { get; init; }
    public string[] Tags { get; init; } = [];
    public string[] ImageUrls { get; init; } = [];
}

public class UpdateSnippetCommandHandler : IRequestHandler<UpdateSnippetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateSnippetCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateSnippetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Snippets
            .Include(s => s.Tags)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
            ?? throw new Exception("Snippet doesn't exists");

        _mapper.Map(request, entity);

        var tags = request.Tags.Select(tag => tag.ToLower())
            .Distinct()
            .ToList();

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

        entity.Tags = allTags;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
