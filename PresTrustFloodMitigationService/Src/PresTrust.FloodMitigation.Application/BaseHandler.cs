using System.Security;

namespace PresTrust.FloodMitigation.Application;

public class BaseHandler
{
    private PermissionEntity permission = default;
    private IApplicationRepository repoApplication;

    public BaseHandler(IApplicationRepository repoApplication)
    {
        this.repoApplication = repoApplication;
    }

    /// <summary>
    /// Get Application for a given Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<FloodApplicationEntity> GetIfApplicationExists(int id)
    {
        var application = await repoApplication.GetApplicationAsync(id);

        if (application == null)
            throw new EntityNotFoundException($"Application (Id: {id}) does not exist or invalid");

        return application;
    }

    public void IsAuthorizedOperation(UserRoleEnum userRole, FloodApplicationEntity application, UserPermissionEnum operation, List<FloodFeedbackEntity> corrections = null)
    {
        var securityMgr = new FloodSecurityManager(userRole, application.Status, corrections);
        permission = securityMgr.Permission;

        VerifyUserAuthorization(operation, userRole);
        VerifyIfOperationIsValidToPerform(operation, application.Status);
    }

    public void IsAuthorizedOperation(UserRoleEnum userRole, ApplicationStatusEnum applicationStatus, UserPermissionEnum operation)
    {
        var securityMgr = new FloodSecurityManager(userRole, applicationStatus);
        permission = securityMgr.Permission;

        VerifyUserAuthorization(operation, userRole);
        VerifyIfOperationIsValidToPerform(operation, applicationStatus);
    }

    /// <summary>
    /// Verify if user is authorized to perform operation
    /// </summary>
    /// <param name="enumPermission"></param>
    private void VerifyUserAuthorization(UserPermissionEnum enumPermission, UserRoleEnum userRole)
    {
        bool authorized = default;

        switch (enumPermission)
        {
            case UserPermissionEnum.CREATE_APPLICATION:
                authorized = permission.CanCreateApplication;
                break;
            case UserPermissionEnum.SUBMIT_DECLARATION_OF_INTENT:
                authorized = permission.CanSubmitDeclarationOfIntent;
                break;
            case UserPermissionEnum.APPROVE_DECLARATION_OF_INTENT:
                authorized = permission.CanApproveDeclarationOfIntent;
                break;
            case UserPermissionEnum.SUBMIT_APPLICATION:
                authorized = permission.CanSubmitApplication;
                break;
            case UserPermissionEnum.REVIEW_APPLICATION:
                authorized = permission.CanReviewApplication;
                break;
            case UserPermissionEnum.ACTIVATE_APPLICATION:
                authorized = permission.CanActivateApplication;
                break;
            case UserPermissionEnum.CLOSE_APPLICATION:
                authorized = permission.CanCloseApplication;
                break;
            case UserPermissionEnum.REJECT_APPLICATION:
                authorized = permission.CanRejectApplication;
                break;
            case UserPermissionEnum.WITHDRAW_APPLICATION:
                authorized = permission.CanWithdrawApplication;
                break;
            case UserPermissionEnum.REINITIATE_APPLICATION:
                authorized = permission.CanReinitiateApplication;
                break;
            case UserPermissionEnum.EDIT_DECLARATION_OF_INTENT_SECTION:
                authorized = permission.CanEditDeclarationOfIntentSection;
                break;
            case UserPermissionEnum.EDIT_ROLES_SECTION:
                authorized = permission.CanEditRolesSection;
                break;
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
                authorized = permission.CanEditOverviewSection;
                break;
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
                authorized = permission.CanEditProjectAreaSection;
                break;
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
                authorized = permission.CanEditFinanceSection;
                break;
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
                authorized = permission.CanEditSignatorySection;
                break;
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:
                authorized = permission.CanEditOtherDocsSection;
                break;
            case UserPermissionEnum.EDIT_ADMIN_DOC_CHK_LIST_SECTION:
                authorized = permission.CanEditAdminDocChkListSection;
                break;
            case UserPermissionEnum.EDIT_ADMIN_DETAILS_SECTION:
                authorized = permission.CanEditAdminDetailsSection;
                break;
            case UserPermissionEnum.EDIT_ADMIN_CONTACTS_SECTION:
                authorized = permission.CanEditAdminContactsSection;
                break;
            case UserPermissionEnum.EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION:
                authorized = permission.CanEditAdminRlsOfFundsSection;
                break;
            case UserPermissionEnum.EDIT_COMMENTS:
                authorized = permission.CanEditComments;
                break;
            case UserPermissionEnum.DELETE_COMMENTS:
                authorized = permission.CanDeleteComments;
                break;
            case UserPermissionEnum.EDIT_FEEDBACK:
                authorized = permission.CanEditFeedback;
                break;
            case UserPermissionEnum.DELETE_FEEDBACKS:
                authorized = permission.CanDeleteFeedback;
                break;
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
                authorized = permission.CanRequestForAnApplicationCorrection;
                break;
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
                authorized = permission.CanRespondToTheRequestForAnApplicationCorrection;
                break;
            case UserPermissionEnum.SAVE_DOCUMENT:
                authorized = permission.CanSaveDocument;
                break;
            case UserPermissionEnum.DELETE_DOCUMENT:
                authorized = permission.CanDeleteDocument;
                break;
            default:
                break;
        }
        if (!authorized)
            throw new UnauthorizedAccessException("Unauthorized operation.");
    }

    /// <summary>
    /// Verify if operation is valid to perform at state of an application
    /// </summary>
    /// <param name="enumPermission"></param>
    /// <param name="applicationStatus"></param>
    private void VerifyIfOperationIsValidToPerform(UserPermissionEnum enumPermission, ApplicationStatusEnum applicationStatus)
    {
        bool isValidOperation = false;

        switch (applicationStatus)
        {
            case ApplicationStatusEnum.NONE:
                if (enumPermission == UserPermissionEnum.CREATE_APPLICATION)
                    isValidOperation = true;
                break;
            case ApplicationStatusEnum.DECLARATION_OF_INTENT_DRAFT:
                isValidOperation = DOIDraftStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.DECLARATION_OF_INTENT_SUBMITTED:
                isValidOperation = DOISubmittedStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.DRAFT:
                isValidOperation = DraftStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.SUBMITTED:
                isValidOperation = SubmittedStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.IN_REVIEW:
                isValidOperation = InReviewStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.ACTIVE:
                isValidOperation = ActiveStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.CLOSED:
                isValidOperation = ClosedStatePermission(enumPermission);
                break;
            case ApplicationStatusEnum.REJECTED:
                isValidOperation = RejectedStatePermissions(enumPermission);
                break;
            case ApplicationStatusEnum.WITHDRAWN:
                isValidOperation = WithdrawnStatePermissions(enumPermission);
                break;
            default:
                break;
        }

        if (!isValidOperation)
            throw new UnauthorizedAccessException("Unauthorized operation.");
    }

    private bool DOIDraftStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_DECLARATION_OF_INTENT_SECTION:

            case UserPermissionEnum.SUBMIT_DECLARATION_OF_INTENT:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool DOISubmittedStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_DECLARATION_OF_INTENT_SECTION:

            case UserPermissionEnum.APPROVE_DECLARATION_OF_INTENT:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            case UserPermissionEnum.REJECT_APPLICATION:
            
            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool DraftStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_ROLES_SECTION:
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:

            case UserPermissionEnum.SUBMIT_APPLICATION:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            
            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool SubmittedStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_ROLES_SECTION:
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:

            case UserPermissionEnum.EDIT_ADMIN_DOC_CHK_LIST_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_DETAILS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_CONTACTS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION:

            case UserPermissionEnum.REVIEW_APPLICATION:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            case UserPermissionEnum.REJECT_APPLICATION:


            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool InReviewStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_ROLES_SECTION:
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:

            case UserPermissionEnum.EDIT_ADMIN_DOC_CHK_LIST_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_DETAILS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_CONTACTS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION:

            case UserPermissionEnum.ACTIVATE_APPLICATION:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            case UserPermissionEnum.REJECT_APPLICATION:


            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool ActiveStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_ROLES_SECTION:
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:

            case UserPermissionEnum.EDIT_ADMIN_DOC_CHK_LIST_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_DETAILS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_CONTACTS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION:

            case UserPermissionEnum.CLOSE_APPLICATION:
            case UserPermissionEnum.WITHDRAW_APPLICATION:
            case UserPermissionEnum.REJECT_APPLICATION:


            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool ClosedStatePermission(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.EDIT_ROLES_SECTION:
            case UserPermissionEnum.EDIT_OVERVIEW_SECTION:
            case UserPermissionEnum.EDIT_PROJECTAREA_SECTION:
            case UserPermissionEnum.EDIT_FINANCE_SECTION:
            case UserPermissionEnum.EDIT_SIGNATORY_SECTION:
            case UserPermissionEnum.EDIT_OTHER_DOCS_SECTION:

            case UserPermissionEnum.EDIT_ADMIN_DOC_CHK_LIST_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_DETAILS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_CONTACTS_SECTION:
            case UserPermissionEnum.EDIT_ADMIN_RELEASE_OF_FUNDS_SECTION:

            case UserPermissionEnum.EDIT_COMMENTS:
            case UserPermissionEnum.DELETE_COMMENTS:
            case UserPermissionEnum.EDIT_FEEDBACK:
            case UserPermissionEnum.DELETE_FEEDBACKS:
            case UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION:
            case UserPermissionEnum.SAVE_DOCUMENT:
            case UserPermissionEnum.DELETE_DOCUMENT:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }

    private bool RejectedStatePermissions(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.REINITIATE_APPLICATION:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }


    private bool WithdrawnStatePermissions(UserPermissionEnum enumPermission)
    {
        bool flag = false;

        switch (enumPermission)
        {
            case UserPermissionEnum.REINITIATE_APPLICATION:
                flag = true;
                break;
            default:
                break;
        }

        return flag;
    }
}