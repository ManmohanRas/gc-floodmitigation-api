namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelAuditDialogQueryValidator: AbstractValidator<GetParcelAuditDialogQuery>
{
    public GetParcelAuditDialogQueryValidator()
    {
        RuleFor(query => query.AgencyId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("AgencyId is required.")
                .GreaterThan(0)
                .WithMessage("AgencyId must be greater than 0");
    }
}
