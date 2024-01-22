namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalCommentCommandValidator: AbstractValidator<SaveMunicipalCommentCommand>
{
    public SaveMunicipalCommentCommandValidator()
    {

        RuleFor(query => query.AgencyId)
            .GreaterThan(0)
            .WithMessage("AgencyId must be greater than 0");
    }
}
