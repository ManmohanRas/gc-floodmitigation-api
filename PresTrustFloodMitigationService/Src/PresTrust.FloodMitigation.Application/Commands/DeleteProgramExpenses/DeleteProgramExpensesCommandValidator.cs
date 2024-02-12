namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteProgramExpensesCommandValidator : AbstractValidator<DeleteProgramExpensesCommand>
{
    public DeleteProgramExpensesCommandValidator()
    {
        RuleFor(query => query.Id)
           .GreaterThan(0)
           .WithMessage("Not a valid ExpenseId");

    }
}
