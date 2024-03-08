namespace Application.Snippets.Commands.UpdateSnippet;

public class UpdateSnippetValidator : AbstractValidator<UpdateSnippetCommand>
{
    public UpdateSnippetValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Title)
            .NotEmpty();

        RuleFor(v => v.Language)
            .NotEmpty();

        RuleFor(v => v.Code)
            .NotEmpty();
    }
}
