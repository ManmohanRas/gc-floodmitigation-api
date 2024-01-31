namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryQueryMappingProfile: Profile
{
    public GetParcelHistoryQueryMappingProfile()
    {
        CreateMap <FloodParcelHistoryEntity, GetParcelHistoryQueryViewModel>();
    }
}
