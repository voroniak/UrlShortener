using FluentValidation;

namespace UrlShortener.Application.Urls.Commands.CreateUrl
{
    public class CreateUrlCommandValidator : AbstractValidator<CreateUrlCommand>
    {
        public CreateUrlCommandValidator()
        {
            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("Url is required.")
                .NotNull();
        }
    }
}