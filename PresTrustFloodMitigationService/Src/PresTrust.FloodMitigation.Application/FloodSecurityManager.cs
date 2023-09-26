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
    private List<FloodApplicationFeedbackEntity> corrections = new List<FloodApplicationFeedbackEntity>();

    public FloodSecurityManager(UserRoleEnum userRole, ApplicationStatusEnum applicationStatus, List<FloodApplicationFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.applicationStatus = applicationStatus;
        this.corrections = corrections ?? new List<FloodApplicationFeedbackEntity>();

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
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanSubmitApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.ROLES,
                    RouterLink = RouterLinks.ROLES_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
                permission.CanSubmitApplication = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.ROLES,
                    RouterLink = RouterLinks.ROLES_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_EDITOR:
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.ROLES,
                    RouterLink = RouterLinks.ROLES_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                Roles();
                //Overview
                Overview();
                //Project Area
                ProjectArea();
                //Finance
                Finance();
                //Signatory
                Signatory();
                //Other Documents
                OtherDocuments();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = NavigationItemTitles.ROLES,
                    RouterLink = RouterLinks.ROLES_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveSubmittedStatePermissions()
    {
    }

    private void DeriveInReviewStatePermissions()
    {
    }

    private void DeriveActiveStatePermissions()
    {
    }

    private void DeriveClosedStatePermissions()
    {
    }

    private void DeriveRejectedStatePermissions()
    {
    }

    private void DeriveWithdrawnStatePermissions()
    {
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

    private void Roles(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewRolesSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.ROLES, RouterLink = RouterLinks.ROLES_VIEW, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditRolesSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.ROLES, RouterLink = RouterLinks.ROLES_EDIT, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void Overview(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewOverviewSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.OVERVIEW, RouterLink = RouterLinks.OVERVIEW_VIEW, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditOverviewSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.OVERVIEW, RouterLink = RouterLinks.OVERVIEW_EDIT, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void ProjectArea(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewProjectAreaSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.PROJECT_AREA, RouterLink = RouterLinks.PROJECT_AREA_VIEW, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditProjectAreaSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.PROJECT_AREA, RouterLink = RouterLinks.PROJECT_AREA_EDIT, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void Finance(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewFinanceSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.FINANCE, RouterLink = RouterLinks.FINANCE_VIEW, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditFinanceSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.FINANCE, RouterLink = RouterLinks.FINANCE_EDIT, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void Signatory(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewSignatorySection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.SIGNATORY, RouterLink = RouterLinks.SIGNATORY_VIEW, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditSignatorySection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.SIGNATORY, RouterLink = RouterLinks.SIGNATORY_EDIT, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void OtherDocuments(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewOtherDocsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.OTHER_DOCUMENTS, RouterLink = RouterLinks.OTHER_DOCUMENTS_VIEW, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditOtherDocsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = NavigationItemTitles.OTHER_DOCUMENTS, RouterLink = RouterLinks.OTHER_DOCUMENTS_EDIT, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }
}
