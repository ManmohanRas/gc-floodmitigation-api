namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQuery : IRequest<IEnumerable<GetFloodParcelsByFilterQueryViewModel>>
{
        public int AgencyId { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string Address { get; set; }
}
