namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryCommandValidator: AbstractValidator<SaveParcelHistoryCommand>
{
    public SaveParcelHistoryCommandValidator()
    {

        RuleFor(query => query.ParcelId)
            .GreaterThan(0)
            .WithMessage("ParcelId must be greater than 0");
    }
}
