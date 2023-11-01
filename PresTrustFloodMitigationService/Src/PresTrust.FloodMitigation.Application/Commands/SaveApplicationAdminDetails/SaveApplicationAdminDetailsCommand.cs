namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationAdminDetailsCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public DateTime? MunicipalResolutionDate { get; set; }
    public string? MunicipalResolutionNumber { get; set; }
    public DateTime? FMCPreliminaryApprovalDate { get; set; }
    public string? FMCPreliminaryNumber { get; set; }
    public DateTime? BCCPreliminaryApprovalDate { get; set; }
    public string? BCCPreliminaryNumber { get; set; }
    public string? ProjectDescription { get; set; }
    public DateTime? FundingExpirationDate { get; set; }
    public DateTime? FirstFundingExpirationDate { get; set; }
    public DateTime? SecondFundingExpirationDate { get; set; }
    public DateTime? CommissionerMeetingDate { get; set; }
    public DateTime? FirstCommitteeMeetingDate { get; set; }
    public DateTime? SecondCommitteeMeetingDate { get; set; }
}
