namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteMunicipalCommentCommandValidator : AbstractValidator<DeleteMunicipalCommentCommand>
{
    public DeleteMunicipalCommentCommandValidator()
    {
        RuleFor(query => query.AgencyId)
           .GreaterThan(0)
           .WithMessage("Not a valid AgencyId");

        RuleFor(query => query.Id)
            .GreaterThan(0)
            .WithMessage("Not a valid CommentId");
    }
}