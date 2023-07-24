namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public string Title { get; set; }
    public int AgencyId { get; set; }
    public string ApplicationType { get; set; }
    public string ApplicationSubType { get; set; }
    public List<string> PamsPins { get; set; }
    public List<FloodApplicationUserViewModel> ApplicationUsers { get; set; }
}
