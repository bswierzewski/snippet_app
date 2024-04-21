using Application.Common.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public Cloudinary Cloudinary { get; }

        public ImageService(IConfiguration configuration)
        {
            Account account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            Cloudinary = new Cloudinary(account);
            Cloudinary.Api.Secure = true;
        }

        public Uri UploadImage(string name, Stream stream)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };
            
            var uploadResult = Cloudinary.Upload(uploadParams);
            
            return uploadResult.Url;
        }

    }
}
