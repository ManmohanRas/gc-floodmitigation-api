namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// create rules for attributes
/// </summary>
public class SaveDocumentDetailsCommandValidator: AbstractValidator<SaveDocumentDetailsCommand>
{
    public SaveDocumentDetailsCommandValidator() 
    {
        RuleFor(command => command.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id.");

        RuleFor(query => query.DocumentType)
             .NotNull().NotEmpty()
             .Must(x => ValidDocumentType(x)).WithMessage("Not a valid document type.");
    }

    /// <summary>
    /// Check if a given document type is valid
    /// </summary>
    /// <param name="docType"></param>
    /// <returns></returns>
    public bool ValidDocumentType(string docType)
    {
        bool result = false;
        Enum.TryParse(value: docType, ignoreCase: true, out DocumentTypeEnum enumDocType);

        if (enumDocType > 0)
            result = true;

        return result;
    }
}
