namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// create rules for attributes
/// </summary>
public class DeleteApplicationDocumentCommandValidator: AbstractValidator<DeleteApplicationDocumentCommand>
{
    public DeleteApplicationDocumentCommandValidator() 
    { 
       RuleFor(command => command.ApplicationId)
            .GreaterThan(0).WithMessage("Not a valid Application Id");

        RuleFor(command => command.Id)
             .GreaterThan(0).WithMessage("Not a valid Document Id");
    }
}
