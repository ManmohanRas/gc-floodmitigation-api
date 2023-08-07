namespace PresTrust.FloodMitigation.Application;

public enum ViewOrEdit
{
    VIEW = 1,
    EDIT = 2
}

public class FloodSecurityManager
{
    private UserRoleEnum userRole = default;
    private ApplicationStatusEnum applicationStatus = default;
    private PermissionEntity permission = default;
    private List<NavigationItemEntity> navigationItems = default;
    private List<NavigationItemEntity> adminNavigationItems = default;
    private List<NavigationItemEntity> postApprovedNavigationItems = default;
    private NavigationItemEntity defaultNavigationItem = default;
    private List<FloodFeedbackEntity> corrections = new List<FloodFeedbackEntity>();

    public FloodSecurityManager(UserRoleEnum userRole, ApplicationStatusEnum applicationStatus, List<FloodFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.applicationStatus = applicationStatus;
        this.corrections = corrections ?? new List<FloodFeedbackEntity>();

        ConfigurePermissions();
    }

    public PermissionEntity Permission { get { return permission; } }
    public List<NavigationItemEntity> NavigationItems { get => navigationItems; }
    public List<NavigationItemEntity> AdminNavigationItems { get => adminNavigationItems; }
    public List<NavigationItemEntity> PostApprovedNavigationItems { get => postApprovedNavigationItems; }
    public NavigationItemEntity DefaultNavigationItem { get => defaultNavigationItem; }

    private void ConfigurePermissions()
    {
        permission = new PermissionEntity();
        navigationItems = new List<NavigationItemEntity>();
        adminNavigationItems = new List<NavigationItemEntity>();
        postApprovedNavigationItems = new List<NavigationItemEntity>();

        if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
        {
            permission.CanCreateApplication = true;
        }

        switch (applicationStatus)
        {
            case ApplicationStatusEnum.NONE:
                break;
            case ApplicationStatusEnum.DOI_DRAFT:
                DeriveDOIDraftStatePermissions();
                break;
            case ApplicationStatusEnum.DOI_SUBMITTED:
                DeriveDOISubmittedStatePermissions();
                break;
            case ApplicationStatusEnum.DRAFT:
                DeriveDraftStatePermissions();
                break;
            case ApplicationStatusEnum.SUBMITTED:
                DeriveSubmittedStatePermissions();
                break;
            case ApplicationStatusEnum.IN_REVIEW:
                DeriveInReviewStatePermissions();
                break;
            case ApplicationStatusEnum.ACTIVE:
                DeriveActiveStatePermissions();
                break;
            case ApplicationStatusEnum.CLOSED:
                DeriveClosedStatePermissions();
                break;
            case ApplicationStatusEnum.REJECTED:
                DeriveRejectedStatePermissions();
                break;
            case ApplicationStatusEnum.WITHDRAWN:
                DeriveWithdrawnStatePermissions();
                break;
            default:
                break;
        }
    }

    private void DeriveDOIDraftStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanSubmitDeclarationOfIntent = true;
                permission.CanWithdrawApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
                permission.CanSubmitDeclarationOfIntent = true;
                permission.CanWithdrawApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_EDITOR:
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveDOISubmittedStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanApproveDeclarationOfIntent = true;
                permission.CanRejectApplication = true;
                permission.CanRequestForAnApplicationCorrection = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                permission.CanViewFeedback = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanRequestForAnApplicationCorrection = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                permission.CanViewFeedback = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                permission.CanViewFeedback = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveDraftStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveSubmittedStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveInReviewStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveActiveStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveClosedStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveRejectedStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveWithdrawnStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReinitiateApplication = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeclarationOfIntent(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewDeclarationOfIntentSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.DECLARATION_OF_INTENT, RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditDeclarationOfIntentSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.DECLARATION_OF_INTENT, RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }
}
