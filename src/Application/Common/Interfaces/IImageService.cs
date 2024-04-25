namespace Application.Common.Interfaces
{
    public interface IImageService
    {
        Uri ImageUpload(string name, Stream stream);
    }
}
