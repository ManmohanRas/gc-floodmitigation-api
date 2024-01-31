namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelAuditDialogQuery : IRequest<IEnumerable<GetParcelAuditDialogQueryViewModel>>
{
    public int AgencyId { get; set; }
}


