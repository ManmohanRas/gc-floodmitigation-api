namespace PresTrust.FloodMitigation.Application.Commands;

public class PreservePropertyCommand : IRequest<PreservePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
