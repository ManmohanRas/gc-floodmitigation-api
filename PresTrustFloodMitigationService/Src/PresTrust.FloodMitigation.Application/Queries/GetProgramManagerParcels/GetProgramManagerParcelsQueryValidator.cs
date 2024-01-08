namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQueryValidator : AbstractValidator<GetProgramManagerParcelsQuery>
{
    public GetProgramManagerParcelsQueryValidator()
    {
        RuleFor(query => query.PageNumber)
                .NotNull().NotEmpty().WithMessage("Page Number is required.")
                .GreaterThan(0).WithMessage("Page Number must be greater than 0");
        RuleFor(query => query.PageRows)
                .NotNull().NotEmpty().WithMessage("Page Rows is required.")
                .GreaterThan(0).WithMessage("Page Rows must be greater than 0");
    }
}
