using PresTrust.FloodMitigation.Domain;
using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Application.Queries; 
public class GetTechDetailsQueryValidator : AbstractValidator<GetTechDetailsQuery>
{
    public GetTechDetailsQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
