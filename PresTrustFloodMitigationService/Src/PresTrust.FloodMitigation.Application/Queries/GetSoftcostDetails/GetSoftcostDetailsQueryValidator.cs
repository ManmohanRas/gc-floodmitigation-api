namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftcostDetailsQueryValidator : AbstractValidator<GetSoftcostDetailsQuery>
{
    public GetSoftcostDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
                .GreaterThan(0)
                .WithMessage("Invalid Application Id");

        RuleFor(query => query.PamsPin)
                .NotEmpty().WithMessage("Invalid PamsPin.");
    }

}
