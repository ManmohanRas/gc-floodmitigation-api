namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationSignatoryCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Designation { get; set; }
    public string Title { get; set; }
    public DateTime? SignedOn { get; set; }
}
