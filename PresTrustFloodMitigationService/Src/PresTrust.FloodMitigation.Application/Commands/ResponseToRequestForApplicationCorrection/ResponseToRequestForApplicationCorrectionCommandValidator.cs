namespace PresTrust.FloodMitigation.Application.Commands;

public class ResponseToRequestForApplicationCorrectionCommandValidator : AbstractValidator<ResponseToRequestForApplicationCorrectionCommand>
{
    /// <summary>
    /// create rules for attributes
    /// </summary>
    public ResponseToRequestForApplicationCorrectionCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(query => query.Sections)
            .NotNull().Must(list => list.Count > 0).WithMessage("Please include at least one corrected section name.");


        RuleFor(command => command.Pamspin)
           .NotEmpty()
           .WithMessage("Not a valid Pamspin");

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
        ApplicationSectionEnum enumSectionType;
        Enum.TryParse(section, ignoreCase: true, out enumSectionType);
        if (enumSectionType > 0)
            result = true;
        return result;
    }

}
