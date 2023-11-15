namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyBrokenRulesQueryViewModel
{
    public int ApplicationId { get; set; }
    public string Section { get; set; }
    public string Message { get; set; }
    public string RouterLink { get; set; }
    public string? PamsPin { get; set; }
}
