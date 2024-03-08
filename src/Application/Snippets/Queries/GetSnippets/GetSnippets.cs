using Application.Common.Interfaces;
using AutoMapper;
using System.ComponentModel;

namespace Application.Snippets.Queries.GetSnippets
{
    public record GetSnippetsQuery : IRequest<List<SnippetDto>>
    {
        public string SearchTerm { get; init; } = "";

        [DisplayName("lang")]
        public string Language { get; init; } = "";

        [DisplayName("tags")]
        public string[] Tags { get; init; } = [];
    }

    public class GetSnippetsQueryHandler : IRequestHandler<GetSnippetsQuery, List<SnippetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetSnippetsQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<List<SnippetDto>> Handle(GetSnippetsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Snippets
                .Include(t => t.Tags)
                .Where(snippet => snippet.CreatedBy == _user.Id);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                query = query.Where(snippet =>
                    EF.Functions.Like(snippet.Title, $"%{request.SearchTerm}%")
                    || snippet.Tags.Any(tag => EF.Functions.Like(tag.Name, $"%{request.SearchTerm}%")));

            if (request.Tags.Length != 0)
                query = query.Where(snippet => snippet.Tags.Any(tag => request.Tags.Contains(tag.Name)));

            if (!string.IsNullOrWhiteSpace(request.Language))
                query = query.Where(snippet => snippet.Language.ToLower() == request.Language.ToLower());

            var result = await query.ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<SnippetDto>>(result);
        }
    }
}