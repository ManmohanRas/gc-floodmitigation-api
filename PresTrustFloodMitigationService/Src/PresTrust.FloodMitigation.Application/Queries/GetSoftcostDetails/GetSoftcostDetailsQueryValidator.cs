namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQueryValidator : AbstractValidator<GetSoftCostDetailsQuery>
{
    public GetSoftCostDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Invalid Application Id");

        RuleFor(query => query.PamsPin)
                .NotEmpty().WithMessage("Invalid PamsPin.");
    }

}
