using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Images.Commands.ImageUpload;

public record ImageUploadCommand() : IRequest<string>
{
    public IFormFile File { get; init; }
}

public class ImageUploadCommandHandler : IRequestHandler<ImageUploadCommand, string>
{
    private readonly IImageService _imageService;

    public ImageUploadCommandHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<string> Handle(ImageUploadCommand request, CancellationToken cancellationToken)
    {
        var url = _imageService.ImageUpload(request.File.FileName, request.File.OpenReadStream());

        return url.AbsoluteUri;
    }
}