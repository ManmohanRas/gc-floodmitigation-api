namespace PresTrust.FloodMitigation.Application.Queries;

public class GetOverviewDetailsQueryValidator : AbstractValidator<GetOverviewDetailsQuery>
{
    public GetOverviewDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("ApplicationId is required.")
                .GreaterThan(0)
                .WithMessage("ApplicationId must be greater than 0");
    }
}
