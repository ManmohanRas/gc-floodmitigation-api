namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class validates query input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class GetBrokenRulesQueryValidator: AbstractValidator<GetBrokenRulesQuery>
{
    public GetBrokenRulesQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
              .GreaterThan(0)
              .WithMessage("Not a valid Application Id");
    }
}
