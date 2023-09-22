namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProjectAreaCommandViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string Title { get; set; }
    public int ApplicationTypeId { get; set; }
    public string ApplicationType { get; set; }
    public int ApplicationSubTypeId { get; set; }
    public string ApplicationSubType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; }
    public string PrevStatus { get; set; }
    public bool CreatedByProgramAdmin { get; set; }
    public FloodAgencyEntity Agency { get; set; }
    public IEnumerable<FloodApplicationCommentEntity> Comments { get; set; }
    public IEnumerable<FloodApplicationFeedbackEntity> Feedbacks { get; set; }
    public PermissionEntity Permission { get; set; } = new PermissionEntity();
    public IEnumerable<NavigationItemEntity> NavigationItems { get; set; } = new List<NavigationItemEntity>();
    public IEnumerable<NavigationItemEntity> AdminNavigationItems { get; set; } = new List<NavigationItemEntity>();
    public IEnumerable<NavigationItemEntity> PostApprovedNavigationItems { get; set; } = new List<NavigationItemEntity>();
    public NavigationItemEntity DefaultNavigationItem { get; set; } = new NavigationItemEntity();
}
