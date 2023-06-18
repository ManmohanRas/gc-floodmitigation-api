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

}
