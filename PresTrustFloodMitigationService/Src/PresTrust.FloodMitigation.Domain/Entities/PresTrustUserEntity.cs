namespace PresTrust.FloodMitigation.Domain.Entities;

public class PresTrustUserEntity
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Title { get; set; }
    public string Role { get; set; }
    public bool IsEnabled { get; set; }
    public string? Status { get; set; }
}
