namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// create rules for attributes
/// </summary>
public class GetDocumentsBySectionDetailsQueryValidator: AbstractValidator<GetDocumentsBySectionDetailsQuery>
{
    public GetDocumentsBySectionDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
