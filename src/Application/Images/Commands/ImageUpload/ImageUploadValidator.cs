namespace Application.Images.Commands.ImageUpload;

public class ImageUploadValidator : AbstractValidator<ImageUploadCommand>
{
    public ImageUploadValidator()
    {
        RuleFor(v => v.File)
            .NotEmpty();
    }
}
