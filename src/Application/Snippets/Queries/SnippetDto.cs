using AutoMapper;
using Domain.Entities;

namespace Application.Snippets.Queries;

public class SnippetDto
{
    public int Id { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Language { get; init; }
    public string Code { get; init; }
    public string Docs { get; init; }
    public bool IsPinned { get; init; }
    public string[] Tags { get; set; }
    public string[] ImageUrls { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Snippet, SnippetDto>()
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.Tags.Select(x => x.Name)));
        }
    }
}
