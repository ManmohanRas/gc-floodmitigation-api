namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class validates command input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
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