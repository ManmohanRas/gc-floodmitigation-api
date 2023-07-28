namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitDeclarationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
}
