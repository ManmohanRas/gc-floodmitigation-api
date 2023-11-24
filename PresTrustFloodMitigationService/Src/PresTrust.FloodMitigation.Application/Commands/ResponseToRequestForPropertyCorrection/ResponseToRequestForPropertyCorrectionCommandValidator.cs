namespace PresTrust.FloodMitigation.Application.Commands;

public class ResponseToRequestForPropertyCorrectionCommandValidator : AbstractValidator<ResponseToRequestForPropertyCorrectionCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public ResponseToRequestForPropertyCorrectionCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(query => query.Sections)
            .NotNull().Must(list => list.Count > 0).WithMessage("Please include at least one corrected section name.");


        RuleFor(command => command.PamsPin)
           .NotEmpty()
           .WithMessage("Not a valid PamsPin");

        RuleForEach(query => query.Sections).ChildRules(section =>
        {
            section.RuleFor(section => section)
                 .NotNull().NotEmpty()
                 .Must(x => ValidSectionType(x)).WithMessage("Invalid application's section");
        });

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    public bool ValidSectionType(string section)
    {
        bool result = false;
        PropertySectionEnum enumSectionType;
        Enum.TryParse(section, ignoreCase: true, out enumSectionType);
        if (enumSectionType > 0)
            result = true;
        return result;
    }

}
