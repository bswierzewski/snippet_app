using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Images.Commands.ImageUpload;

public record ImageUploadCommand() : IRequest<List<string>>
{
    public IFormFileCollection Files { get; init; }
}

public class ImageUploadCommandHandler : IRequestHandler<ImageUploadCommand, List<string>>
{
    private readonly IImageService _imageService;

    public ImageUploadCommandHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<List<string>> Handle(ImageUploadCommand request, CancellationToken cancellationToken)
    {
        var urls = new List<string>();

        foreach (var file in request.Files)
        {
            var url = _imageService.ImageUpload(file.FileName, file.OpenReadStream());

            urls.Add(url.AbsoluteUri);
        }
       
        return urls;
    }
}