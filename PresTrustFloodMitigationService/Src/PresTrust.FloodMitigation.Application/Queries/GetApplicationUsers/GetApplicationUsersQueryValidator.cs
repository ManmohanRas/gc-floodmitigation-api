namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// create rules for attributes
/// </summary>
public class GetApplicationUsersQueryValidator: AbstractValidator<GetApplicationUsersQuery>
{
    public GetApplicationUsersQueryValidator()
    {
        RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
    }
}
