using Application.Common.Interfaces;
using Domain.Events;

namespace Application.Snippets.Commands.DeleteSnippet;

public record DeleteSnippetCommand(int Id) : IRequest<int>;

public class DeleteSnippetCommandHandler : IRequestHandler<DeleteSnippetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteSnippetCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(DeleteSnippetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Snippets.FindAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new Exception("Snippet not exist");

        if(entity.CreatedBy != _user.Id)
            throw new Exception("Only creator can delete snippet");

        _context.Snippets.Remove(entity);

        entity.AddDomainEvent(new SnippetDeletedEvent(entity));

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

