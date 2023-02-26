using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
