namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDetailsQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int AgencyId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string StreetNo { get; set; }
    public string StreetAddress { get; set; }
    public string PropertyAddress { get; set; }
    public decimal Acreage { get; set; }
    public string LandOwner { get; set; }
    public string OwnersAddress1 { get; set; }
    public string OwnersAddress2 { get; set; }
    public string OwnersCity { get; set; }
    public string OwnersState { get; set; }
    public string OwnersZipcode { get; set; }
    public int SquareFootage { get; set; }
    public int YearOfConstruction { get; set; }
    public string TargetArea { get; set; }
    public bool IsFLAP { get; set; }
    public DateTime? DateOfFLAP { get; set; }
    public bool IsValidPamsPin { get; set; }
    public string Status { get; set; }
    public string PrevStatus { get; set; }
    public bool IsLocked { get; set; }
    public bool AlreadyExists { get; set; }
    public IEnumerable<FloodPropertyCommentEntity> Comments { get; set;}
    public IEnumerable<FloodPropertyFeedbackEntity> Feedbacks { get; set;}
    public PropertyPermissionEntity Permission { get; set; } = new PropertyPermissionEntity();
    public IEnumerable<NavigationItemEntity> NavigationItems { get; set; } = new List<NavigationItemEntity>();
    public IEnumerable<NavigationItemEntity> AdminNavigationItems { get; set; } = new List<NavigationItemEntity>();
    public IEnumerable<NavigationItemEntity> PostApprovedNavigationItems { get; set; } = new List<NavigationItemEntity>();
    public NavigationItemEntity DefaultNavigationItem { get; set; } = new NavigationItemEntity();
}
