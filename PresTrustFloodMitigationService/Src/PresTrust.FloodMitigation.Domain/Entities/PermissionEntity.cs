namespace PresTrust.FloodMitigation.Domain.Entities;

public class PermissionEntity
{
    //---------------------------------------------------------------------//
    //  Application Section Permissions
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
    //  Application Admin Action Section Permissions
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
    //  Application Status Permissions
    //---------------------------------------------------------------------//

    public bool CanCreateApplication { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSubmitDeclarationOfIntent { get; set; } = false;
    public bool CanApproveDeclarationOfIntent { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSubmitApplication { get; set; } = false;
    public bool CanReviewApplication { get; set; } = false;
    public bool CanActivateApplication { get; set; } = false;
    public bool CanCloseApplication { get; set; } = false;
    public bool CanRejectApplication { get; set; } = false;
    public bool CanWithdrawApplication { get; set; } = false;
    public bool CanReinitiateApplication { get; set; } = false;

    //---------------------------------------------------------------------//
    //  Application Details Permissions
    //---------------------------------------------------------------------//

    public bool CanViewComments { get; set; } = false;
    public bool CanEditComments { get; set; } = false;
    public bool CanDeleteComments { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanViewFeedback { get; set; } = false;
    public bool CanEditFeedback { get; set; } = false;
    public bool CanDeleteFeedback { get; set; } = false;
    public bool CanRequestForAnApplicationCorrection { get; set; } = false;
    public bool CanRespondToTheRequestForAnApplicationCorrection { get; set; } = false;

    //---------------------------------------------------------------------//

    public bool CanSaveDocument { get; set; } = false;
    public bool CanDeleteDocument { get; set; } = false;
}
