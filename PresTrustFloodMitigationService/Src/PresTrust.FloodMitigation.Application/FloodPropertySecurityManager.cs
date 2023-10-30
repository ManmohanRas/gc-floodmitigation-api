using Newtonsoft.Json.Linq;
using PresTrust.FloodMitigation.Domain.Enums;

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
    private PropertyPermissionEntity permission = default;
    private List<NavigationItemEntity> navigationItems = default;
    private List<NavigationItemEntity> adminNavigationItems = default;
    private List<NavigationItemEntity> postApprovedNavigationItems = default;
    private NavigationItemEntity defaultNavigationItem = default;
    private List<FloodPropertyFeedbackEntity> corrections = new List<FloodPropertyFeedbackEntity>();

    public FloodPropertySecurityManager(UserRoleEnum userRole, PropertyStatusEnum propertyStatus, List<FloodPropertyFeedbackEntity> corrections = null)
    {
        this.userRole = userRole;
        this.propertyStatus = propertyStatus;
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

        if (userRole == UserRoleEnum.AGENCY_ADMIN || userRole == UserRoleEnum.PROGRAM_ADMIN)
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
                permission.CanSubmitProperty = true;
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
                permission.CanSubmitProperty = true;
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
            case UserRoleEnum.AGENCY_EDITOR:
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
                    RouterLink = PropertyRouterLinks.PROPERTY_VIEW,
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

    private void DerivePendingStatePermissions()
    {
    }

    private void DeriveApprovedStatePermissions()
    {
    }

    private void DerivePreservedStatePermissions()
    {
    }

    private void DeriveGrantExpiredStatePermissions()
    {
    }

    private void DeriveRejectedStatePermissions()
    {
    }

    private void DeriveWithdrawnStatePermissions()
    {
    }

    private void DeriveProjectAreaExpiredStatePermissions()
    {
    }

    private void DeriveTransferredStatePermissions()
    {
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
}
