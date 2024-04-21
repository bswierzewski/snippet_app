namespace Application.Common.Interfaces
{
    public interface IImageService
    {
        Uri UploadImage(string name, Stream stream);
    }
}
