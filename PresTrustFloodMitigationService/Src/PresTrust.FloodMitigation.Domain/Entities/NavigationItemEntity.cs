namespace PresTrust.FloodMitigation.Domain.Entities;

public class NavigationItemEntity
{
    public short Id { get; set; }
    public short ParentId { get; set; }
    public string Icon { get; set; }
    public string RouterLink { get; set; }
    public int SortOrder { get; set; }
    public string Title { get; set; }
}
