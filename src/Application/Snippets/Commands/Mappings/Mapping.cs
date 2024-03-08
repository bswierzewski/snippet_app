using Application.Snippets.Commands.CreateSnippet;
using Application.Snippets.Commands.UpdateSnippet;
using AutoMapper;
using Domain.Entities;

namespace Application.Snippets.Commands.Mappings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UpdateSnippetCommand, Snippet>()
            .ForMember(d => d.Tags, m => m.Ignore());

        CreateMap<CreateSnippetCommand, Snippet>()
            .ForMember(d => d.Tags, m => m.Ignore());
    }
}
