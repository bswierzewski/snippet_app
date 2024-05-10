using Application.Images.Commands.ImageUpload;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Endpoints
{
    public class Images : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .DisableAntiforgery()
                .RequireAuthorization()
                .MapPost(UploadImage);
        }

        public async Task<string> UploadImage(ISender sender, [FromForm] ImageUploadCommand command)
        {
            return await sender.Send(command);
        }
    }
}
