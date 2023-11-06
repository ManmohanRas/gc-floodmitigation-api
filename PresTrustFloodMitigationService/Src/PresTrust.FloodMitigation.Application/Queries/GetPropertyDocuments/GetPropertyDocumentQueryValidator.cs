namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentQueryValidator : AbstractValidator<GetPropertyDocumentQuery>

    {
        public GetPropertyDocumentQueryValidator() 
        {
            RuleFor(query => query.ApplicationId)
               .GreaterThan(0).WithMessage("Not a valid Application Id");

            RuleFor(query => query.Pamspin).NotEmpty().WithMessage("Not a valid PamsPin");
        }    
    }
}
