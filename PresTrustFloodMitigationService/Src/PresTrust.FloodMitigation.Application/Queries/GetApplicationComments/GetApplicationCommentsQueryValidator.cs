namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationCommentsQueryValidator: AbstractValidator<GetApplicationCommentsQuery>
{
    public GetApplicationCommentsQueryValidator() 
    {
        RuleFor(query => query.ApplicationId)
                .Cascade(CascadeMode.Stop)
                .NotNull().NotEmpty().WithMessage("ApplicationId is required.")
                .GreaterThan(0)
                .WithMessage("ApplicationId must be greater than 0");
    }
}
