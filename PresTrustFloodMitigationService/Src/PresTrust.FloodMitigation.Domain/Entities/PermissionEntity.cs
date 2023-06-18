namespace PresTrust.FloodMitigation.Domain.Entities;

public class PermissionEntity
{
    public bool CanCreateApplication { get; set; } = false;
    public bool CanSubmitDeclarationOfIntent { get; set; } = false;
    public bool CanApproveDeclarationOfIntent { get; set; } = false;
    public bool CanSignTheDeclarationOfIntent { get; set; } = false;

    public bool CanViewDeclarationOfIntentSection { get; set; } = false;
    public bool CanEditDeclarationOfIntentSection { get; set; } = false;

    public bool CanViewPropertySection { get; set; } = false;
    public bool CanEditPropertySection { get; set; } = false;

    public bool CanViewUserRolesSection { get; set; } = false;
    public bool CanEditUserRolesSection { get; set; } = false;

    public bool CanViewBudgetSection { get; set; } = false;
    public bool CanEditBudgetSection { get; set; } = false;

    public bool CanViewScopeOfWorkSection { get; set; } = false;
    public bool CanEditScopeOfWorkSection { get; set; } = false;

    public bool CanViewPublicBenefitSection { get; set; } = false;
    public bool CanEditPublicBenefitSection { get; set; } = false;

    public bool CanViewSignaturesSection { get; set; } = false;
    public bool CanEditSignaturesSection { get; set; } = false;

    public bool CanViewOtherDocsSection { get; set; } = false;
    public bool CanEditOtherDocsSection { get; set; } = false;

    public bool CanViewGrantAgreementSection { get; set; } = false;
    public bool CanEditGrantAgreementSection { get; set; } = false;
    public bool CanDeleteGrantAgreementDocuments { get; set; } = false;

    public bool CanViewEasementSection { get; set; } = false;
    public bool CanEditEasementSection { get; set; } = false;

    public bool CanViewAdminRvwCmntsSection { get; set; } = false;
    public bool CanEditAdminRvwCmntsSection { get; set; } = false;

    public bool CanViewAdminDocChkListSection { get; set; } = false;
    public bool CanEditAdminDocChkListSection { get; set; } = false;

    public bool CanViewAdminPendingSection { get; set; } = false;
    public bool CanEditAdminPendingSection { get; set; } = false;

    public bool CanViewAdminEasementsSection { get; set; } = false;
    public bool CanEditAdminEasementsSection { get; set; } = false;

    public bool CanViewAdminRlsOfFundsSection { get; set; } = false;
    public bool CanEditAdminRlsOfFundsSection { get; set; } = false;

    public bool CanEditPriority { get; set; } = false;

    public bool CanViewComments { get; set; } = false;
    public bool CanEditComments { get; set; } = false;
    public bool CanDeleteComments { get; set; } = false;

    public bool CanViewConsultantComments { get; set; } = false;
    public bool CanEditConsultantComments { get; set; } = false;
    public bool CanDeleteConsultantComments { get; set; } = false;

    public bool CanViewFeedback { get; set; } = false;
    public bool CanEditFeedback { get; set; } = false;
    public bool CanDeleteFeedback { get; set; } = false;
    public bool CanRequestForAnApplicationCorrection { get; set; } = false;
    public bool CanRespondToTheRequestForAnApplicationCorrection { get; set; } = false;

    public bool CanViewConsultantFeedback { get; set; } = false;
    public bool CanEditConsultantFeedback { get; set; } = false;
    public bool CanDeleteConsultantFeedbacks { get; set; } = false;
    public bool CanRespondToTheConsultantFeedback { get; set; } = false;

    public bool CanSaveDocument { get; set; } = false;
    public bool CanDeleteDocument { get; set; } = false;

    public bool CanApplicantSignHSRReport { get; set; } = false;
    public bool CanArchitectSignHSRReport { get; set; } = false;

    public bool CanApplicantSignTeamMembersCheckList { get; set; } = false;
    public bool CanArchitectSignTeamMembersCheckList { get; set; } = false;

    public bool CanApplicantSignOwnersAssurance { get; set; } = false;

    public bool CanSubmitApplication { get; set; } = false;
    public bool CanAcceptApplicationWithoutAward { get; set; } = false;
    public bool CanSubmitApplicationToReviewBoard { get; set; }
    public bool CanSubmitApplicationToCommissionerMeeting { get; set; }
    public bool CanApprove { get; set; }
    public bool CanComplete { get; set; }

    public bool CanWithdrawApplication { get; set; } = false;
    public bool CanRejectApplication { get; set; } = false;
    public bool CanDeclineApplication { get; set; } = false;
    public bool CanExpire { get; set; } = false;
    public bool CanExpireGrant { get; set; } = false;
    public bool CanExpireEasement { get; set; } = false;
    public bool CanReinitiate { get; set; } = false;
    public bool CanUpload75Document { get; set; } = false;

    public bool CanCommitteeReview { get; set; } = false;

}
