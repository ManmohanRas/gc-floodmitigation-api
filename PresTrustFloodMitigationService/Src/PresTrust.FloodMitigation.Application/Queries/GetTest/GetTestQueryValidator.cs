namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetTestQueryValidator : AbstractValidator<GetTestQuery>
    {
        public GetTestQueryValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Not a valid Id");
        }
    }
}
