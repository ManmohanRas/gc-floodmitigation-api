namespace PresTrust.FloodMitigation.Application.Commands;

public class GrantExpirePropertyCommand : IRequest<GrantExpirePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
