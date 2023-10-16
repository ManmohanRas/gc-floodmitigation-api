namespace PresTrust.FloodMitigation.Domain.Entities;

public class PropertyPermissionEntity
{
    //---------------------------------------------------------------------//
    //  Property Section Permissions
    //---------------------------------------------------------------------//
    public bool CanViewDeclarationOfIntentSection { get; set; } = false;
    public bool CanEditDeclarationOfIntentSection { get; set; } = false;
    
    //---------------------------------------------------------------------//

    public bool CanViewRolesSection { get; set; } = false;
    public bool CanEditRolesSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewOverviewSection { get; set; } = false;
    public bool CanEditOverviewSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewProjectAreaSection { get; set; } = false;
    public bool CanEditProjectAreaSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewFinanceSection { get; set; } = false;
    public bool CanEditFinanceSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewSignatorySection { get; set; } = false;
    public bool CanEditSignatorySection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewOtherDocsSection { get; set; } = false;
    public bool CanEditOtherDocsSection { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Property Admin Action Section Permissions
    //---------------------------------------------------------------------//

    public bool CanViewAdminDocChkListSection { get; set; } = false;
    public bool CanEditAdminDocChkListSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminDetailsSection { get; set; } = false;
    public bool CanEditAdminDetailsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminContactsSection { get; set; } = false;
    public bool CanEditAdminContactsSection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewAdminRlsOfFundsSection { get; set; } = false;
    public bool CanEditAdminRlsOfFundsSection { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Property Status Permissions
    //---------------------------------------------------------------------//

    public bool CanCreateProperty { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSubmitDeclarationOfIntent { get; set; } = false;
    public bool CanApproveDeclarationOfIntent { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSubmitProperty { get; set; } = false;
    public bool CanReviewProperty { get; set; } = false;
    public bool CanActivateProperty { get; set; } = false;
    public bool CanCloseProperty { get; set; } = false;
    public bool CanRejectProperty { get; set; } = false;
    public bool CanWithdrawProperty { get; set; } = false;
    public bool CanReinitiateProperty { get; set; } = false;

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
