namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFundingAgencyCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
    public string? FundingAgencyName { get; set; }
    public string? CurrentStatus { get; set; }
    public DateTime? DateOfApproval { get; set; }
}
