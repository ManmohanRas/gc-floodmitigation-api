namespace PresTrust.FloodMitigation.Application.Queries;

public class GetBrokenRulesQueryViewModel
{
    public int ApplicationId { get; set; }
    public string Section { get; set; }
    public string Message { get; set; }
    public string RouterLink { get; set; }
}
