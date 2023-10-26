namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodPropertyAdminDetailsEntity
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string PamsPin { get; set; } = "";
    public string LastUpdatedBy { get; set; } = "";
}
