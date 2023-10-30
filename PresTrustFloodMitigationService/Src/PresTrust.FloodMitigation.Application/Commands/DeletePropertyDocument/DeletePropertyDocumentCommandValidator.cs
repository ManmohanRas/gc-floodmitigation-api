namespace PresTrust.FloodMitigation.Application.Commands
{ 
    public class DeletePropertyDocumentCommandValidator: AbstractValidator<DeletePropertyDocumentCommand>
    {
        public DeletePropertyDocumentCommandValidator()
        {
            RuleFor(command => command.ApplicationId)
                 .GreaterThan(0).WithMessage("Not a valid Application Id");

            RuleFor(command => command.Id)
                 .GreaterThan(0).WithMessage("Not a valid Document Id");

            RuleFor(command => command.Pamspin)
                .NotEmpty().WithMessage("Not a valid PamsPin");
        }

    }
}
