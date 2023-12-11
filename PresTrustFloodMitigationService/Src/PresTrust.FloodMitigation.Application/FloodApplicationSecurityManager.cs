namespace PresTrust.FloodMitigation.Application;

public enum ViewOrEdit
{
    VIEW = 1,
    EDIT = 2
}

public class FloodApplicationSecurityManager
{
    private UserRoleEnum userRole = default;
    private ApplicationStatusEnum applicationStatus = default;
    private ApplicationStatusEnum applicationPrevStatus = default;
    private ApplicationPermissionEntity permission = default;
    private List<NavigationItemEntity> navigationItems = default;
    private List<NavigationItemEntity> adminNavigationItems = default;
    private List<NavigationItemEntity> postApprovedNavigationItems = default;
    private NavigationItemEntity defaultNavigationItem = default;
    private List<FloodApplicationFeedbackEntity> corrections = new List<FloodApplicationFeedbackEntity>();

    public FloodApplicationSecurityManager(UserRoleEnum userRole, ApplicationStatusEnum applicationStatus, ApplicationStatusEnum applicationPrevStatus, List<FloodApplicationFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.applicationStatus = applicationStatus;
        this.applicationPrevStatus = applicationPrevStatus;
        this.corrections = corrections ?? new List<FloodApplicationFeedbackEntity>();

        ConfigurePermissions();
    }

    public ApplicationPermissionEntity Permission { get { return permission; } }
    public List<NavigationItemEntity> NavigationItems { get => navigationItems; }
    public List<NavigationItemEntity> AdminNavigationItems { get => adminNavigationItems; }
    public List<NavigationItemEntity> PostApprovedNavigationItems { get => postApprovedNavigationItems; }
    public NavigationItemEntity DefaultNavigationItem { get => defaultNavigationItem; }

    private void ConfigurePermissions()
    {
        permission = new ApplicationPermissionEntity();
        navigationItems = new List<NavigationItemEntity>();
        adminNavigationItems = new List<NavigationItemEntity>();
        postApprovedNavigationItems = new List<NavigationItemEntity>();

        if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN || userRole == UserRoleEnum.PROGRAM_EDITOR)
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
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanSubmitDeclarationOfIntent = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanSubmitDeclarationOfIntent = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveDOISubmittedStatePermissions()
    {
        FloodApplicationFeedbackEntity correction = default;
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanApproveDeclarationOfIntent = true;
                    permission.CanRejectApplication = true;
                    permission.CanRequestForAnApplicationCorrection = true;
                    permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                    permission.CanEditFeedback = true;
                    permission.CanDeleteFeedback = true;
                }
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.DECLARATION_OF_INTENT).FirstOrDefault();
                if (correction == null)
                {
                    // Declaration Of Intent
                    DeclarationOfIntent(enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT,
                        SortOrder = 1
                    };
                }
                else
                {
                    // Declaration Of Intent
                    if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                    {
                        DeclarationOfIntent(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                        // Default Navigation Item
                        this.defaultNavigationItem = new NavigationItemEntity()
                        {
                            Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                            RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT,
                            SortOrder = 1
                        };
                    }
                    else
                    {
                        DeclarationOfIntent(correction: true);
                        // Default Navigation Item
                        this.defaultNavigationItem = new NavigationItemEntity()
                        {
                            Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                            RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                            SortOrder = 1
                        };
                    }
                }
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                }
                permission.CanViewFeedback = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.DECLARATION_OF_INTENT).FirstOrDefault();
                if (correction == null)
                {
                    // Declaration Of Intent
                    DeclarationOfIntent();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                        SortOrder = 1
                    };
                }
                else
                {
                    // Declaration Of Intent
                    DeclarationOfIntent(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT,
                        SortOrder = 1
                    };
                }
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
                // Declaration Of Intent
                DeclarationOfIntent();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                    RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
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
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanSubmitApplication = true;
                }
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
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory();
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.ROLES,
                    RouterLink = ApplicationRouterLinks.ROLES_EDIT,
                    SortOrder = 2
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanSubmitApplication = true;
                }
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
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory();
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.ROLES,
                    RouterLink = ApplicationRouterLinks.ROLES_EDIT,
                    SortOrder = 2
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
                if (userRole == UserRoleEnum.AGENCY_SIGNATORY)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory();
                //Other Documents
                OtherDocuments();
                // Default Navigation Item
                if (userRole == UserRoleEnum.AGENCY_SIGNATORY)
                {
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.SIGNATORY,
                        RouterLink = ApplicationRouterLinks.SIGNATORY_EDIT,
                        SortOrder = 6
                    };
                }
                else
                {
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.ROLES,
                        RouterLink = ApplicationRouterLinks.ROLES_VIEW,
                        SortOrder = 2
                    };
                }
                break;
        }
    }

    private void DeriveSubmittedStatePermissions()
    {
        FloodApplicationFeedbackEntity correction = default;
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReviewApplication = true;
                permission.CanRequestForAnApplicationCorrection = true;
                permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                    SortOrder = 7
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                }
                permission.CanViewFeedback = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles();
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview();
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea();
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance();
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
        }
    }

    private void DeriveInReviewStatePermissions()
    {
        FloodApplicationFeedbackEntity correction = default;
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanActivateApplication = true;
                permission.CanRejectApplication = true;
                permission.CanRequestForAnApplicationCorrection = true;
                permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                    SortOrder = 7
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                }
                permission.CanViewFeedback = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles();
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview();
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea();
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance();
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
        }
    }

    private void DeriveActiveStatePermissions()
    {
        FloodApplicationFeedbackEntity correction = default;
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanCloseApplication = true;
                permission.CanWithdrawApplication = true;
                permission.CanRequestForAnApplicationCorrection = true;
                permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                    SortOrder = 7
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Roles(correction: true);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Overview(correction: true);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    ProjectArea(correction: true);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Finance(correction: true);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Contacts
                AdminContacts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanRespondToTheRequestForAnApplicationCorrection = true;
                }
                permission.CanViewFeedback = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                // Declaration Of Intent
                DeclarationOfIntent();
                //Roles
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.ROLES).FirstOrDefault();
                if (correction == null)
                    Roles();
                else
                    Roles(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Overview
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OVERVIEW).FirstOrDefault();
                if (correction == null)
                    Overview();
                else
                    Overview(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Project Area
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.PROJECT_AREA).FirstOrDefault();
                if (correction == null)
                    ProjectArea();
                else
                    ProjectArea(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.FINANCE).FirstOrDefault();
                if (correction == null)
                    Finance();
                else
                    Finance(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Signatory
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.SIGNATORY).FirstOrDefault();
                if (correction == null)
                    Signatory();
                else
                    Signatory(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                correction = this.corrections.Where(c => c.Section == ApplicationSectionEnum.OTHER_DOCUMENTS).FirstOrDefault();
                if (correction == null)
                {
                    OtherDocuments();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                }
                else
                {
                    OtherDocuments(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT,
                        SortOrder = 7
                    };
                }
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
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
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
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
                //Admin Document Checklist
                AdminDocumentChecklist();
                //Admin Details
                AdminDetails();
                //Admin Contacts
                AdminContacts();
                //Admin Release of Funds
                AdminReleaseOfFunds();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                permission.CanViewFeedback = true;
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
        }
    }

    private void DeriveRejectedStatePermissions()
    {
        // DOI Reject
        if (applicationPrevStatus == ApplicationStatusEnum.DOI_SUBMITTED)
        {
            switch (userRole)
            {
                case UserRoleEnum.SYSTEM_ADMIN:
                case UserRoleEnum.PROGRAM_ADMIN:
                case UserRoleEnum.PROGRAM_EDITOR:
                    if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                    {
                        permission.CanReinitiateApplication = true;
                    }
                    permission.CanViewFeedback = true;
                    permission.CanViewComments = true;
                    permission.CanEditComments = true;
                    permission.CanDeleteComments = true;
                    // Declaration Of Intent
                    DeclarationOfIntent();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                        SortOrder = 1
                    };
                    break;
                case UserRoleEnum.AGENCY_ADMIN:
                case UserRoleEnum.AGENCY_EDITOR:
                    permission.CanViewFeedback = true;
                    // Declaration Of Intent
                    DeclarationOfIntent();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                        SortOrder = 1
                    };
                    break;
                default:
                    if (userRole != UserRoleEnum.AGENCY_READONLY)
                    {
                        permission.CanViewFeedback = true;
                    }
                    // Declaration Of Intent
                    DeclarationOfIntent();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT,
                        RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW,
                        SortOrder = 1
                    };
                    break;
            }
        }
        // Application Reject
        else
        {
            switch (userRole)
            {
                case UserRoleEnum.SYSTEM_ADMIN:
                case UserRoleEnum.PROGRAM_ADMIN:
                case UserRoleEnum.PROGRAM_EDITOR:
                    if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                    {
                        permission.CanReinitiateApplication = true;
                    }
                    permission.CanViewFeedback = true;
                    permission.CanViewComments = true;
                    permission.CanEditComments = true;
                    permission.CanDeleteComments = true;
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
                    //Admin Document Checklist
                    AdminDocumentChecklist();
                    //Admin Details
                    AdminDetails();
                    //Admin Contacts
                    AdminContacts();
                    //Admin Release of Funds
                    AdminReleaseOfFunds();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                    break;
                case UserRoleEnum.AGENCY_ADMIN:
                case UserRoleEnum.AGENCY_EDITOR:
                    permission.CanViewFeedback = true;
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
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                    break;
                default:
                    if (userRole != UserRoleEnum.AGENCY_READONLY)
                    {
                        permission.CanViewFeedback = true;
                    }
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
                        Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                        RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                        SortOrder = 7
                    };
                    break;
            }
        }
    }

    private void DeriveWithdrawnStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanCloseApplication = true;
                    permission.CanReinitiateApplication = true;
                }
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
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
                //Admin Document Checklist
                AdminDocumentChecklist();
                //Admin Details
                AdminDetails();
                //Admin Contacts
                AdminContacts();
                //Admin Release of Funds
                AdminReleaseOfFunds();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                permission.CanViewFeedback = true;
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
                };
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
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
                    Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS,
                    RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW,
                    SortOrder = 7
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT, RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_VIEW, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditDeclarationOfIntentSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.DECLARATION_OF_INTENT, RouterLink = ApplicationRouterLinks.DECLARATION_OF_INTENT_EDIT, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ROLES, RouterLink = ApplicationRouterLinks.ROLES_VIEW, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditRolesSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ROLES, RouterLink = ApplicationRouterLinks.ROLES_EDIT, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.OVERVIEW, RouterLink = ApplicationRouterLinks.OVERVIEW_VIEW, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditOverviewSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.OVERVIEW, RouterLink = ApplicationRouterLinks.OVERVIEW_EDIT, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.PROJECT_AREA, RouterLink = ApplicationRouterLinks.PROJECT_AREA_VIEW, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditProjectAreaSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.PROJECT_AREA, RouterLink = ApplicationRouterLinks.PROJECT_AREA_EDIT, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.FINANCE, RouterLink = ApplicationRouterLinks.FINANCE_VIEW, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditFinanceSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.FINANCE, RouterLink = ApplicationRouterLinks.FINANCE_EDIT, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.SIGNATORY, RouterLink = ApplicationRouterLinks.SIGNATORY_VIEW, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditSignatorySection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.SIGNATORY, RouterLink = ApplicationRouterLinks.SIGNATORY_EDIT, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS, RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_VIEW, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditOtherDocsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.OTHER_DOCUMENTS, RouterLink = ApplicationRouterLinks.OTHER_DOCUMENTS_EDIT, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminDocumentChecklist(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminDocChkListSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_DOCUMENT_CHECKLIST, RouterLink = ApplicationRouterLinks.ADMIN_DOCUMENT_CHECKLIST_VIEW, SortOrder = 8, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminDocChkListSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_DOCUMENT_CHECKLIST, RouterLink = ApplicationRouterLinks.ADMIN_DOCUMENT_CHECKLIST_EDIT, SortOrder = 8, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminDetails(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminDetailsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_DETAILS, RouterLink = ApplicationRouterLinks.ADMIN_DETAILS_VIEW, SortOrder = 9, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminDetailsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_DETAILS, RouterLink = ApplicationRouterLinks.ADMIN_DETAILS_EDIT, SortOrder = 9, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminContacts(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminContactsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_CONTACTS, RouterLink = ApplicationRouterLinks.ADMIN_CONTACTS_VIEW, SortOrder = 10, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminContactsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_CONTACTS, RouterLink = ApplicationRouterLinks.ADMIN_CONTACTS_EDIT, SortOrder = 10, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminReleaseOfFunds(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminRlsOfFundsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_RELEASE_OF_FUNDS, RouterLink = ApplicationRouterLinks.ADMIN_RELEASE_OF_FUNDS_VIEW, SortOrder = 11, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminRlsOfFundsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = ApplicationNavigationItemTitles.ADMIN_RELEASE_OF_FUNDS, RouterLink = ApplicationRouterLinks.ADMIN_RELEASE_OF_FUNDS_EDIT, SortOrder = 11, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }
}
