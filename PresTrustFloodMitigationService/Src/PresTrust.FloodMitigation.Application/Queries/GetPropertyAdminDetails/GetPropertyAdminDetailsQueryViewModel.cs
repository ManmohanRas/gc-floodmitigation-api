namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string?  PamsPin { get; set; }
    public DateTime? DobDocumentsMissingDate { get; set; }
    public DateTime?  FmcFinalApprovalDate { get; set; }
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
    public DateTime? FmcSoftcostReimbApprovalDate { get; set; }
    public string? FmcSoftcostReimbApprovalNumber { get; set; }
    public DateTime? BccSoftcostReimbApprovalDate { get; set; }
    public string? BccSoftcostReimbApprovalNumber { get; set; }
    public bool DoesHomeOwnerHaveNFIPInsurance { get; set; }
    public bool IsDEPInvolved { get; set; }
    public bool IsPARRequestedbyFunder { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
}
