namespace PresTrust.FloodMitigation.Application.Commands;

public class ApproveDeclarationCommand : IRequest<ApproveDeclarationCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
