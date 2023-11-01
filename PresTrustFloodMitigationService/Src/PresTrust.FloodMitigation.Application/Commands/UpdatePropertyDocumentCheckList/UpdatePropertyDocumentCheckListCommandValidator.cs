using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Application.Commands
{
    public class UpdatePropertyDocumentCheckListCommandValidator : AbstractValidator<UpdatePropertyDocumentCheckListCommand> 
    { 



    public UpdatePropertyDocumentCheckListCommandValidator()
    {
        RuleFor(command => command.ApplicationId)
           .GreaterThan(0).WithMessage("Not a valid Application Id");

        //RuleForEach(command => command.Documents).SetValidator(new DocumentValidator());

        RuleForEach(command => command.Documents).ChildRules(docs =>
        {
            docs.RuleFor(doc => doc.FileName).NotNull().NotEmpty().WithMessage("One or more document's name has not been provided.");
            docs.RuleFor(doc => doc.Title).NotNull().NotEmpty().WithMessage("One or more document's title  is required.");
            docs.RuleFor(doc => doc.DocumentType)
                    .NotNull().NotEmpty()
                    .Must(docType => ValidDocumentType(docType)).WithMessage("One or more document's types are not valid.");
            docs.RuleFor(doc => doc.Section)
                .NotNull().NotEmpty()
                .Must(section => ValidPropertySection(section)).WithMessage("One or more document's section-names are not valid.");
        });
    }

    /// <summary>
    /// Check if a given document type is valid
    /// </summary>
    /// <param name="docType"></param>
    /// <returns></returns>
    public bool ValidDocumentType(string docType)
    {
        bool result = false;
        Enum.TryParse(value: docType, ignoreCase: true, out PropertyDocumentTypeEnum enumDocType);

        if (enumDocType > 0)
            result = true;

        return result;
    }

    /// <summary>
    /// Check if a given application section is valid
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    public bool ValidPropertySection(string section)
    {
        bool result = false;
        Enum.TryParse(value: section, ignoreCase: true, out PropertySectionEnum enumSection);

        if (enumSection > 0)
            result = true;

        return result;
    }
}

public class propertyDocumentValidator : AbstractValidator<PropertyDocumentViewModel>
{
    public propertyDocumentValidator()
    {
        RuleFor(query => query.FileName)
           .NotNull().NotEmpty()
           .WithMessage("Document FileName  is required.");

        RuleFor(query => query.Title)
           .NotNull().NotEmpty()
           .WithMessage("Document Title is required.");

        RuleFor(query => query.DocumentType)
         .NotNull().NotEmpty()
         .Must(x => ValidPropertyDocumentType(x)).WithMessage("Not a valid document type.");
    }

    /// <summary>
    /// Check if a given document type is valid
    /// </summary>
    /// <param name="docType"></param>
    /// <returns></returns>
    public bool ValidPropertyDocumentType(string docType)
    {
        bool result = false;
        Enum.TryParse(value: docType, ignoreCase: true, out PropertyDocumentTypeEnum enumDocType);

        if (enumDocType > 0)
            result = true;

        return result;
    }
}
    }

