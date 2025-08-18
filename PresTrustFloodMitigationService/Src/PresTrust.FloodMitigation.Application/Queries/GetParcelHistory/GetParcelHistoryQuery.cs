namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryQuery : IRequest<IEnumerable<GetParcelHistoryQueryViewModel>>
{
    public int ParcelId { get; set; }
    public string UserId { get; set; }
}


