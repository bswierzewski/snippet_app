using Application.Snippets.Commands.CreateSnippet;

namespace Application.Snippets.Commands.CreateTodoItem;

public class UpdateSnippetValidator : AbstractValidator<CreateSnippetCommand>
{
    public UpdateSnippetValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty();

        RuleFor(v => v.Language)
            .NotEmpty();

        RuleFor(v => v.Code)
            .NotEmpty();
    }
}
