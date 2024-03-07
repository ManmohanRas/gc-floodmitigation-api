namespace PresTrust.FloodMitigation.Application.Commands;

public class EmailTriggerCommand: IRequest<bool>
{
    public int ApplicationId { get; set; } = 0;
    public int AgencyId { get; set; } = 0;
    public string? PamsPin { get; set; }
    public string EmailTemplateCode { get; set; }
    public bool? IsProgramManager { get; set; } = false;
    public DateTime? EmailDate { get; set; } = DateTime.Now;
}
