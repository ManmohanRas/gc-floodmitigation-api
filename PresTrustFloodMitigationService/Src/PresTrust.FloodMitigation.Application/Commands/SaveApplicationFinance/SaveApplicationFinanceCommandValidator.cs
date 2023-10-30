namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class validates query input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class SaveApplicationFinanceCommandValidator: AbstractValidator<SaveApplicationFinanceCommand>
{
    public SaveApplicationFinanceCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Not a valid Application Id.");
        
    }
}
