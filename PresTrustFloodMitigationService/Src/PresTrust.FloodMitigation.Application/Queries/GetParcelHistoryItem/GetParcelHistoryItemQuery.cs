namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryItemQuery : IRequest<GetParcelHistoryItemQueryViewModel>
{
    public int ParcelId { get; set; }
}


