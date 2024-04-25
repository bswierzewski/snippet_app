using Application.Images.Commands.ImageUpload;
using MediatR;
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

        public async Task<List<string>> UploadImage(ISender sender, IFormFileCollection files)
        {
            return await sender.Send(new ImageUploadCommand()
            {
                Files = files
            });
        }
    }
}
