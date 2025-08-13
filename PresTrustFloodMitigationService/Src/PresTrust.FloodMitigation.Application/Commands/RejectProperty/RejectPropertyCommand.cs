namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectPropertyCommand : IRequest<RejectPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }  
}
