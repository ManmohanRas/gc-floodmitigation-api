namespace PresTrust.FloodMitigation.Domain.Entities;

public class PropertyPermissionEntity
{
    //---------------------------------------------------------------------//
    //  Property Section Permissions
    //---------------------------------------------------------------------//

    public bool CanViewPropertySection { get; set; } = false;
    public bool CanEditPropertySection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewOtherDocsSection { get; set; } = false;
    public bool CanEditOtherDocsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewSoftCostsSection { get; set; } = false;
    public bool CanEditSoftCostsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewTechSection { get; set; } = false;
    public bool CanEditTechSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewFinanceSection { get; set; } = false;
    public bool CanEditFinanceSection { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Property Admin Action Section Permissions
    //---------------------------------------------------------------------//

    public bool CanViewAdminDocChkListSection { get; set; } = false;
    public bool CanEditAdminDocChkListSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminSurveySection { get; set; } = false;
    public bool CanEditAdminSurveySection { get; set; } = false;
    
    //---------------------------------------------------------------------//

    public bool CanViewAdminDetailsSection { get; set; } = false;
    public bool CanEditAdminDetailsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminRlsOfFundsSection { get; set; } = false;
    public bool CanEditAdminRlsOfFundsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminTrackingSection { get; set; } = false;
    public bool CanEditAdminTrackingSection { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Property Status Permissions
    //---------------------------------------------------------------------//

    public bool CanCreateProperty { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSubmitProperty { get; set; } = false;
    public bool CanReviewProperty { get; set; } = false;
    public bool CanPendProperty { get; set; } = false;
    public bool CanApproveProperty { get; set; } = false;
    public bool CanPreserveProperty { get; set; } = false;
    public bool CanGrantExpireProperty { get; set; } = false;
    public bool CanRejectProperty { get; set; } = false;
    public bool CanWithdrawProperty { get; set; } = false;
    public bool CanProjectAreaExpireProperty { get; set; } = false;
    public bool CanTransferProperty { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Property Details Permissions
    //---------------------------------------------------------------------//

    public bool CanViewComments { get; set; } = false;
    public bool CanEditComments { get; set; } = false;
    public bool CanDeleteComments { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewFeedback { get; set; } = false;
    public bool CanEditFeedback { get; set; } = false;
    public bool CanDeleteFeedback { get; set; } = false;
    public bool CanRequestForAPropertyCorrection { get; set; } = false;
    public bool CanRespondToTheRequestForAPropertyCorrection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSaveDocument { get; set; } = false;
    public bool CanDeleteDocument { get; set; } = false;
}
