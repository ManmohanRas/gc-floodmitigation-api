using PresTrust.FloodMitigation.Domain;
using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Application.Queries; 
public class GetParcelPropertyQueryValidator : AbstractValidator<GetParcelPropertyQuery>
{
    public GetParcelPropertyQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
