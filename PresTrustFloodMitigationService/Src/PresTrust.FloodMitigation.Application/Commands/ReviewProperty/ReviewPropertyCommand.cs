namespace PresTrust.FloodMitigation.Application.Commands;

public class ReviewPropertyCommand : IRequest<ReviewPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string Pamspin { get; set; }
}
