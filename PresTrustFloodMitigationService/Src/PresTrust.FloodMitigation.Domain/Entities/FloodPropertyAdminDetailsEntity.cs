namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodPropertyAdminDetailsEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public DateTime? MunicipalResolutionDate { get; set; }
    public string MunicipalResolutionNumber { get; set; }
    public DateTime? FMCPreliminaryApprovalDate { get; set; }
    public string FMCPreliminaryNumber { get; set; }
    public DateTime? BCCPreliminaryApprovalDate { get; set; }
    public string BCCPreliminaryNumber { get; set; }
    public DateTime? FundingExpirationDate { get; set; }
    public DateTime? FirstFundingExpirationDate { get; set; }
    public DateTime? SecondFundingExpirationDate { get; set; }
    public DateTime? DobDocumentsMissingDate { get; set; }
    public DateTime? FmcFinalApprovalDate { get; set; }
    public string? FmcFinalNumber { get; set; }
    public string? BccFinalNumber { get; set; }
    public DateTime? BccFinalApprovalDate { get; set; }
    public DateTime? MunicipalPurchaseDate { get; set; }
    public string? MunicipalPurchaseNumber { get; set; }
    public DateTime? GrantAgreementDate { get; set; }
    public DateTime? GrantAgreementExpirationDate { get; set; }
    public DateTime? DueDiligenceDocumentsMissingDate { get; set; }
    public DateTime? ScheduleClosingDate { get; set; }
    public DateTime? SoftCostReimbursementRequestDate { get; set; }
    public DateTime? FmcSoftCostReimbApprovalDate { get; set; }
    public string? FmcSoftCostReimbApprovalNumber { get; set; }
    public DateTime? BccSoftCostReimbApprovalDate { get; set; }
    public string? BccSoftCostReimbApprovalNumber { get; set; }
    public bool DoesHomeOwnerHaveNFIPInsurance { get; set; }
    public bool IsDEPInvolved { get; set; }
    public bool IsPARRequestedbyFunder { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
}
