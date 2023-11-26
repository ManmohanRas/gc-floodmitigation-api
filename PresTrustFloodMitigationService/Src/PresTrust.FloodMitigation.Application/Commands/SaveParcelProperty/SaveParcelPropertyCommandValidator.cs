using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelPropertyCommandValidator : AbstractValidator<SaveParcelPropertyCommand>
{
    public SaveParcelPropertyCommandValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
        RuleFor(query => query.PamsPin)
                .NotEmpty()
                .WithMessage("Not a valid PamsPin.");
    }
}