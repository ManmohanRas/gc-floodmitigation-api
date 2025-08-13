namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitDeclarationCommand : IRequest<SubmitDeclarationCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
