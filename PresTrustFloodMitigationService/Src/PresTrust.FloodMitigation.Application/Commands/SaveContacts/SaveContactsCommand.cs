namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactsCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public List<SaveContactsModel> Contacts { get; set; }
}

public class SaveContactsModel
{
    public int Id { get; set; }
    public string? ContactName { get; set; }
    public string? Agency { get; set; }
    public string? Email { get; set; }
    public string? MainNumber { get; set; }
    public string? AlternateNumber { get; set; }
    public bool SelectContact { get; set; }
}
