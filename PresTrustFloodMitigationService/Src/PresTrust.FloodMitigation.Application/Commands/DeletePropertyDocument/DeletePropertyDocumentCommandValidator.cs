namespace PresTrust.FloodMitigation.Application.Commands
{ 
    public class DeletePropertyDocumentCommandValidator: AbstractValidator<DeletePropertyDocumentCommand>
    {
        public DeletePropertyDocumentCommandValidator()
        {
            RuleFor(command => command.Id)
                 .GreaterThan(0).WithMessage("Not a valid Document Id");
        }

    }
}
