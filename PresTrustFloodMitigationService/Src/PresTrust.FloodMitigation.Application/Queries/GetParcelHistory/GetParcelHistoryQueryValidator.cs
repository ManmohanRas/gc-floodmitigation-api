namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryQueryValidator: AbstractValidator<GetParcelHistoryQuery>
{
    public GetParcelHistoryQueryValidator()
    {
        RuleFor(query => query.ParcelId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("ParcelId is required.")
                .GreaterThan(0)
                .WithMessage("ParcelId must be greater than 0");
    }
}
