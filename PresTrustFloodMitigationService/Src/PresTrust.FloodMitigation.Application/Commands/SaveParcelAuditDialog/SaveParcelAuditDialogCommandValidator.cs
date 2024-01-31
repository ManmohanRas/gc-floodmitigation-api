namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelAuditDialogCommandValidator: AbstractValidator<SaveParcelAuditDialogCommand>
{
    public SaveParcelAuditDialogCommandValidator()
    {

        RuleFor(query => query.AgencyId)
            .GreaterThan(0)
            .WithMessage("AgencyId must be greater than 0");
    }
}
