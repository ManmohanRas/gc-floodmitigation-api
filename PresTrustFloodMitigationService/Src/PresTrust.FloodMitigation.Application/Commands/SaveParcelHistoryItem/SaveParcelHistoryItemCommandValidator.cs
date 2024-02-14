namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryItemCommandValidator: AbstractValidator<SaveParcelHistoryItemCommand>
{
    public SaveParcelHistoryItemCommandValidator()
    {

        RuleFor(query => query.ParcelId)
            .GreaterThan(0)
            .WithMessage("ParcelId must be greater than 0");
    }
}
