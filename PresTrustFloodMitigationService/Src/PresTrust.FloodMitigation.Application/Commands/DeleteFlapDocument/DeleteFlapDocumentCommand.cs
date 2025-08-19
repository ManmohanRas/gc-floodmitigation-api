namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFlapDocumentCommand: IRequest<bool>
{
    public int AgencyId { get; set; }
    public int Id { get; set; }
    public string UserId { get; set; }
}
