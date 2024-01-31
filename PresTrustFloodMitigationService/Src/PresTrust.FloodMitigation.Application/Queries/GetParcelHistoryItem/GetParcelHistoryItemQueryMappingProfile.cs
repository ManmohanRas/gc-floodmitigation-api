namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryItemQueryMappingProfile: Profile
{
    public GetParcelHistoryItemQueryMappingProfile()
    {
        CreateMap <FloodParcelHistoryEntity, GetParcelHistoryItemQueryViewModel>();
    }
}
