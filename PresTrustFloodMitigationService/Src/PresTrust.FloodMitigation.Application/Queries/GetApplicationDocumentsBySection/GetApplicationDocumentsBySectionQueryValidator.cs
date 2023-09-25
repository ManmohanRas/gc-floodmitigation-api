namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// create rules for attributes
/// </summary>
public class GetApplicationDocumentsBySectionQueryValidator: AbstractValidator<GetApplicationDocumentsBySectionQuery>
{
    public GetApplicationDocumentsBySectionQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
