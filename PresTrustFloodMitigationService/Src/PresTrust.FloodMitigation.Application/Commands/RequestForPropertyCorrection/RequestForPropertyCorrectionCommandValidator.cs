namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class validates command input
/// </summary>
/// <remarks>
/// Returns BadRequest Response if any failures occured
/// </remarks>
public class RequestForPropertyCorrectionCommandValidator : AbstractValidator<RequestForPropertyCorrectionCommand>
{
        /// <summary>
        /// create rules for attributes
        /// </summary>
        public RequestForPropertyCorrectionCommandValidator()
        {
            RuleFor(command => command.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");
        }
    
}
