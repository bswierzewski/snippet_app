using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Snippets.Queries.GetSnippets
{
    public record GetSnippetQuery(int Id) : IRequest<SnippetDto>;

    public class GetSnippetQueryHandler : IRequestHandler<GetSnippetQuery, SnippetDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetSnippetQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<SnippetDto> Handle(GetSnippetQuery request, CancellationToken cancellationToken)
        {
            var snippet = await _context.Snippets
                .Include(t => t.Tags)
                .Where(snippet =>
                    snippet.CreatedBy == _user.Id && snippet.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<SnippetDto>(snippet);
        }
    }

}
