namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalCommentCommand : IRequest<int>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string? Comment { get; set; }
    public string UserId { get; set; }
}

