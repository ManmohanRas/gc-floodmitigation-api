namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapTargetAreasQueryValidator: AbstractValidator<GetFlapTargetAreasQuery>
{
    public GetFlapTargetAreasQueryValidator()
    {
        RuleFor(query => query.AgencyId)
                .GreaterThan(0)
                .WithMessage("Not a valid Agency Id.");
    }
}
