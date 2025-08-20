namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationUserEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Title { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsPrimaryContact { get; set; }
    public bool IsAlternateContact { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
