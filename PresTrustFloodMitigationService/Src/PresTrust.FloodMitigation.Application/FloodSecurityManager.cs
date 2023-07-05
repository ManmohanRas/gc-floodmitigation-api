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
    private ApplicationStatusEnum previousApplicationStatus = default;
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

    public FloodSecurityManager(UserRoleEnum userRole, ApplicationStatusEnum applicationStatus, ApplicationStatusEnum previousApplicationStatus, List<FloodFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.applicationStatus = applicationStatus;
        this.previousApplicationStatus = previousApplicationStatus;
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
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanSubmitDeclarationOfIntent = true;
                permission.CanWithdrawApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;

                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);

                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_EDITOR:
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanSubmitDeclarationOfIntent = false;
                permission.CanWithdrawApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;

                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);

                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_SIGNATORY:
            case UserRoleEnum.AGENCY_READONLY:
            case UserRoleEnum.PROGRAM_READONLY:
            case UserRoleEnum.SYSTEM_ADMIN:
                DeclarationOfIntent();

                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = RouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };

                break;
            default:
                break;
        }
    }

    private void DeriveDOISubmittedStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveDraftStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveSubmittedStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveInReviewStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveActiveStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveClosedStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveRejectedStatePermissions()
    {
        throw new NotImplementedException();
    }

    private void DeriveWithdrawnStatePermissions()
    {
        throw new NotImplementedException();
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
