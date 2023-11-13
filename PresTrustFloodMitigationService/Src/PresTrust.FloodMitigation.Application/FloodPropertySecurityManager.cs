namespace PresTrust.FloodMitigation.Application;

//public enum ViewOrEdit
//{
//    VIEW = 1,
//    EDIT = 2
//}

public class FloodPropertySecurityManager
{
    private UserRoleEnum userRole = default;
    private PropertyStatusEnum propertyStatus = default;
    private PropertyStatusEnum propertyPrevStatus = default;
    private ApplicationStatusEnum applicationStatus = default;
    private PropertyPermissionEntity permission = default;
    private List<NavigationItemEntity> navigationItems = default;
    private List<NavigationItemEntity> adminNavigationItems = default;
    private List<NavigationItemEntity> postApprovedNavigationItems = default;
    private NavigationItemEntity defaultNavigationItem = default;
    private List<FloodPropertyFeedbackEntity> corrections = new List<FloodPropertyFeedbackEntity>();

    public FloodPropertySecurityManager(UserRoleEnum userRole, PropertyStatusEnum propertyStatus, PropertyStatusEnum propertyPrevStatus, ApplicationStatusEnum applicationStatus, List<FloodPropertyFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.propertyStatus = propertyStatus;
        this.propertyPrevStatus = propertyPrevStatus;
        this.applicationStatus = applicationStatus;
        this.corrections = corrections ?? new List<FloodPropertyFeedbackEntity>();

        ConfigurePermissions();
    }

    public PropertyPermissionEntity Permission { get { return permission; } }
    public List<NavigationItemEntity> NavigationItems { get => navigationItems; }
    public List<NavigationItemEntity> AdminNavigationItems { get => adminNavigationItems; }
    public List<NavigationItemEntity> PostApprovedNavigationItems { get => postApprovedNavigationItems; }
    public NavigationItemEntity DefaultNavigationItem { get => defaultNavigationItem; }

    private void ConfigurePermissions()
    {
        permission = new PropertyPermissionEntity();
        navigationItems = new List<NavigationItemEntity>();
        adminNavigationItems = new List<NavigationItemEntity>();
        postApprovedNavigationItems = new List<NavigationItemEntity>();

        if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
        {
            permission.CanCreateProperty = true;
        }

        switch (propertyStatus)
        {
            case PropertyStatusEnum.NONE:
                DeriveNoneStatePermissions();
                break;
            case PropertyStatusEnum.SUBMITTED:
                DeriveSubmittedStatePermissions();
                break;
            case PropertyStatusEnum.IN_REVIEW:
                DeriveInReviewStatePermissions();
                break;
            case PropertyStatusEnum.PENDING:
                DerivePendingStatePermissions();
                break;
            case PropertyStatusEnum.APPROVED:
                DeriveApprovedStatePermissions();
                break;
            case PropertyStatusEnum.PRESERVED:
                DerivePreservedStatePermissions();
                break;
            case PropertyStatusEnum.GRANT_EXPIRED:
                DeriveGrantExpiredStatePermissions();
                break;
            case PropertyStatusEnum.REJECTED:
                DeriveRejectedStatePermissions();
                break;
            case PropertyStatusEnum.WITHDRAWN:
                DeriveWithdrawnStatePermissions();
                break;
            case PropertyStatusEnum.PROJECT_AREA_EXPIRED:
                DeriveProjectAreaExpiredStatePermissions();
                break;
            case PropertyStatusEnum.TRANSFERRED:
                DeriveTransferredStatePermissions();
                break;
            default:
                break;
        }
    }

    private void DeriveNoneStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanSubmitProperty = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.AGENCY_ADMIN:
            case UserRoleEnum.AGENCY_EDITOR:
                if (userRole == UserRoleEnum.AGENCY_ADMIN)
                {
                    permission.CanSubmitProperty = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveSubmittedStatePermissions()
    {
        FloodPropertyFeedbackEntity correction = default;
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanReviewProperty = true;
                permission.CanRequestForAPropertyCorrection = true;
                permission.CanRespondToTheRequestForAPropertyCorrection = true;
                permission.CanEditFeedback = true;
                permission.CanDeleteFeedback = true;
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                correction = this.corrections.Where(c => c.Section == PropertySectionEnum.PROPERTY).FirstOrDefault();
                if (correction == null)
                    Property(enumViewOrEdit: ViewOrEdit.EDIT);
                else
                    Property(correction: true, enumViewOrEdit: ViewOrEdit.EDIT);
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Survey
                AdminSurvey(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Tracking
                AdminTracking(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            case UserRoleEnum.PROGRAM_EDITOR:
                permission.CanViewFeedback = true;
                permission.CanViewComments = true;
                permission.CanEditComments = true;
                permission.CanDeleteComments = true;
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Document Checklist
                AdminDocumentChecklist(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Survey
                AdminSurvey(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Details
                AdminDetails(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Release of Funds
                AdminReleaseOfFunds(enumViewOrEdit: ViewOrEdit.EDIT);
                //Admin Tracking
                AdminTracking(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                if (userRole != UserRoleEnum.AGENCY_READONLY)
                {
                    permission.CanViewFeedback = true;
                }
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    permission.CanViewComments = true;
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
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
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanPendProperty = true;
                    permission.CanRejectProperty = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DerivePendingStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanApproveProperty = true;
                    permission.CanWithdrawProperty = true;
                    permission.CanTransferProperty = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.AGENCY_EDITOR)
                {
                    permission.CanSaveDocument = true;
                    permission.CanDeleteDocument = true;
                    //Other Documents
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Soft Costs
                    SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                }
                if (userRole == UserRoleEnum.AGENCY_SIGNATORY || userRole == UserRoleEnum.AGENCY_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveApprovedStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
            case UserRoleEnum.PROGRAM_EDITOR:
                if (userRole == UserRoleEnum.SYSTEM_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
                {
                    permission.CanPreserveProperty = true;
                }
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                //Soft Costs
                SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.AGENCY_EDITOR)
                {
                    permission.CanSaveDocument = true;
                    permission.CanDeleteDocument = true;
                    //Other Documents
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Soft Costs
                    SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                }
                if (userRole == UserRoleEnum.AGENCY_SIGNATORY || userRole == UserRoleEnum.AGENCY_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DerivePreservedStatePermissions()
    {
        if (applicationStatus == ApplicationStatusEnum.CLOSED)
        {
            switch (userRole)
            {
                case UserRoleEnum.SYSTEM_ADMIN:
                case UserRoleEnum.PROGRAM_ADMIN:
                    permission.CanSaveDocument = true;
                    permission.CanDeleteDocument = true;
                    //Property
                    Property(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Finance
                    Finance();
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = PropertyNavigationItemTitles.PROPERTY,
                        RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                        SortOrder = 1
                    };
                    break;
                default:
                    //Property
                    Property();
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                    {
                        //Tech
                        Tech();
                        //Finance
                        Finance();
                    }
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = PropertyNavigationItemTitles.PROPERTY,
                        RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                        SortOrder = 1
                    };
                    break;
            }
        }
        else
        {
            switch (userRole)
            {
                case UserRoleEnum.SYSTEM_ADMIN:
                case UserRoleEnum.PROGRAM_ADMIN:
                case UserRoleEnum.PROGRAM_EDITOR:
                    permission.CanSaveDocument = true;
                    permission.CanDeleteDocument = true;
                    //Property
                    Property(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Other Documents
                    OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Soft Costs
                    SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Tech
                    Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                    //Finance
                    Finance(enumViewOrEdit: ViewOrEdit.EDIT);
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = PropertyNavigationItemTitles.PROPERTY,
                        RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                        SortOrder = 1
                    };
                    break;
                default:
                    //Property
                    Property();
                    if (userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                    {
                        //Other Documents
                        OtherDocuments();
                        //Soft Costs
                        SoftCosts();
                        //Tech
                        Tech();
                        //Finance
                        Finance();
                    }
                    if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.AGENCY_EDITOR)
                    {
                        permission.CanSaveDocument = true;
                        permission.CanDeleteDocument = true;
                        //Other Documents
                        OtherDocuments(enumViewOrEdit: ViewOrEdit.EDIT);
                        //Soft Costs
                        SoftCosts(enumViewOrEdit: ViewOrEdit.EDIT);
                    }
                    if (userRole == UserRoleEnum.AGENCY_SIGNATORY || userRole == UserRoleEnum.AGENCY_READONLY)
                    {
                        //Other Documents
                        OtherDocuments();
                        //Soft Costs
                        SoftCosts();
                    }
                    // Default Navigation Item
                    this.defaultNavigationItem = new NavigationItemEntity()
                    {
                        Title = PropertyNavigationItemTitles.PROPERTY,
                        RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                        SortOrder = 1
                    };
                    break;
            }
        }
    }

    private void DeriveGrantExpiredStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
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
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                //Tech
                Tech();
                //Finance
                Finance();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Other Documents
                    OtherDocuments();
                    //Soft Costs
                    SoftCosts();
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
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
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveProjectAreaExpiredStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void DeriveTransferredStatePermissions()
    {
        switch (userRole)
        {
            case UserRoleEnum.SYSTEM_ADMIN:
            case UserRoleEnum.PROGRAM_ADMIN:
                permission.CanSaveDocument = true;
                permission.CanDeleteDocument = true;
                //Property
                Property(enumViewOrEdit: ViewOrEdit.EDIT);
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                //Tech
                Tech(enumViewOrEdit: ViewOrEdit.EDIT);
                //Finance
                Finance();
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_EDIT,
                    SortOrder = 1
                };
                break;
            default:
                //Property
                Property();
                //Other Documents
                OtherDocuments();
                //Soft Costs
                SoftCosts();
                if (userRole == UserRoleEnum.PROGRAM_EDITOR || userRole == UserRoleEnum.PROGRAM_COMMITTEE || userRole == UserRoleEnum.PROGRAM_READONLY)
                {
                    //Tech
                    Tech();
                    //Finance
                    Finance();
                }
                // Default Navigation Item
                this.defaultNavigationItem = new NavigationItemEntity()
                {
                    Title = PropertyNavigationItemTitles.PROPERTY,
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
                    SortOrder = 1
                };
                break;
        }
    }

    private void Property(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewPropertySection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.PROPERTY, RouterLink = PropertyRouterLinks.PROPERTY_VIEW, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditPropertySection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.PROPERTY, RouterLink = PropertyRouterLinks.PROPERTY_EDIT, SortOrder = 1, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.OTHER_DOCUMENTS, RouterLink = PropertyRouterLinks.OTHER_DOCUMENTS_VIEW, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditOtherDocsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.OTHER_DOCUMENTS, RouterLink = PropertyRouterLinks.OTHER_DOCUMENTS_EDIT, SortOrder = 2, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void SoftCosts(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewSoftCostsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.SOFT_COSTS, RouterLink = PropertyRouterLinks.SOFT_COSTS_VIEW, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditSoftCostsSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.SOFT_COSTS, RouterLink = PropertyRouterLinks.SOFT_COSTS_EDIT, SortOrder = 3, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void Tech(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewTechSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.TECH, RouterLink = PropertyRouterLinks.TECH_VIEW, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditTechSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.TECH, RouterLink = PropertyRouterLinks.TECH_EDIT, SortOrder = 4, Icon = (correction == true ? "report_problem" : "") });
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
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.FINANCE, RouterLink = PropertyRouterLinks.FINANCE_VIEW, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditFinanceSection = true;
                navigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.FINANCE, RouterLink = PropertyRouterLinks.FINANCE_EDIT, SortOrder = 5, Icon = (correction == true ? "report_problem" : "") });
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
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_DOCUMENT_CHECKLIST, RouterLink = PropertyRouterLinks.ADMIN_DOCUMENT_CHECKLIST_VIEW, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminDocChkListSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_DOCUMENT_CHECKLIST, RouterLink = PropertyRouterLinks.ADMIN_DOCUMENT_CHECKLIST_EDIT, SortOrder = 6, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminSurvey(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminSurveySection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_SURVEY, RouterLink = PropertyRouterLinks.ADMIN_SURVEY_VIEW, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminSurveySection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_SURVEY, RouterLink = PropertyRouterLinks.ADMIN_SURVEY_EDIT, SortOrder = 7, Icon = (correction == true ? "report_problem" : "") });
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
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_DETAILS, RouterLink = PropertyRouterLinks.ADMIN_DETAILS_VIEW, SortOrder = 8, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminDetailsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_DETAILS, RouterLink = PropertyRouterLinks.ADMIN_DETAILS_EDIT, SortOrder = 8, Icon = (correction == true ? "report_problem" : "") });
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
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_RELEASE_OF_FUNDS, RouterLink = PropertyRouterLinks.ADMIN_RELEASE_OF_FUNDS_VIEW, SortOrder = 9, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminRlsOfFundsSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_RELEASE_OF_FUNDS, RouterLink = PropertyRouterLinks.ADMIN_RELEASE_OF_FUNDS_EDIT, SortOrder = 9, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }

    private void AdminTracking(bool correction = false, ViewOrEdit enumViewOrEdit = ViewOrEdit.VIEW)
    {
        switch (enumViewOrEdit)
        {
            case ViewOrEdit.VIEW:
                permission.CanViewAdminTrackingSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_TRACKING, RouterLink = PropertyRouterLinks.ADMIN_TRACKING_VIEW, SortOrder = 10, Icon = (correction == true ? "report_problem" : "") });
                break;
            case ViewOrEdit.EDIT:
                permission.CanEditAdminTrackingSection = true;
                adminNavigationItems.Add(new NavigationItemEntity() { Title = PropertyNavigationItemTitles.ADMIN_TRACKING, RouterLink = PropertyRouterLinks.ADMIN_TRACKING_EDIT, SortOrder = 10, Icon = (correction == true ? "report_problem" : "") });
                break;
            default:
                break;
        }
    }
}
