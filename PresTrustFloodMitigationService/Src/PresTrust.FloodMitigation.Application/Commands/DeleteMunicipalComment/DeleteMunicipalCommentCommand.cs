namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteMunicipalCommentCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
}
