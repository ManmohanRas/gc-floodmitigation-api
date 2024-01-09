namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQuery: IRequest<Unit>
{
    public int AgencyId { get; set; }
    public IFormFile? file { get; set; }
}
