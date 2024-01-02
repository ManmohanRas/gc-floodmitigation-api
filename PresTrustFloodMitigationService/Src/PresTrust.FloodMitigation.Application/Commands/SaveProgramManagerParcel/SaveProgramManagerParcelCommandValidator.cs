using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramManagerParcelCommandValidator : AbstractValidator<SaveProgramManagerParcelCommand>
{
    public SaveProgramManagerParcelCommandValidator()
    {
        RuleFor(query => query.Id)
                .GreaterThan(0)
                .WithMessage("Not a valid Id.");
        RuleFor(query => query.Block)
                .NotEmpty()
                .WithMessage("Not a valid PamsPin.");
        RuleFor(query => query.Lot)
                .NotEmpty()
                .WithMessage("Not a valid PamsPin.");
    }
}