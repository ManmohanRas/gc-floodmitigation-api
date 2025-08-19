namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationReleaseOfFundsCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? CAFNumber { get; set; }
    public bool CAFClosed { get; set; }
    public string? UserId { get; set; }
}
