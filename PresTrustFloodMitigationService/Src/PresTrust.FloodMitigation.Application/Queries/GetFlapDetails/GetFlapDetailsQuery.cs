namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQuery: IRequest<GetFlapDetailsQueryViewModel>
{
    public int AgencyId { get; set; }
}
