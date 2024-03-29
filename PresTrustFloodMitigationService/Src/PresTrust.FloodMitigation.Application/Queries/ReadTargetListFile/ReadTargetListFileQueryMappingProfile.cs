namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryMappingProfile: Profile
{
    public ReadTargetListFileQueryMappingProfile()
    {
        CreateMap<ReadTargerListParcels, FloodParcelEntity>();
    }
}
