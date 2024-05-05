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

        public async Task<string> UploadImage(ISender sender, IFormFile file)
        {
            return await sender.Send(new ImageUploadCommand()
            {
                File = file
            });
        }
    }
}
