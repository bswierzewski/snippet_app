using Application.Snippets.Commands.CreateSnippet;
using Application.Snippets.Commands.DeleteSnippet;
using Application.Snippets.Commands.UpdateSnippet;
using Application.Snippets.Queries;
using Application.Snippets.Queries.GetSnippets;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints;

public class Snippets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetSnippets)
            .MapGet(GetSnippet, "{id}")
            .MapPost(CreateSnippet)
            .MapPut(UpdateSnippet, "{id}")
            .MapDelete(DeleteSnippet, "{id}");
    }

    public async Task<List<SnippetDto>> GetSnippets(ISender sender, string searchTerm)
    {
        return await sender.Send(GetSnippetsExtensions.GetSnippetsQueryWithParsedParams(searchTerm));
    }

    public async Task<SnippetDto> GetSnippet(ISender sender, int id)
    { 
        return await sender.Send(new GetSnippetQuery(id));
    }

    public async Task<int> CreateSnippet(ISender sender, CreateSnippetCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateSnippet(ISender sender, int id, UpdateSnippetCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();        
    }

    public async Task<IResult> DeleteSnippet(ISender sender, int id)
    {
        await sender.Send(new DeleteSnippetCommand(id));
        return Results.NoContent();
    }
}
