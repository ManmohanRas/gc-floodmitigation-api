namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveWarningsCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string WarningType { get; set; }
}
