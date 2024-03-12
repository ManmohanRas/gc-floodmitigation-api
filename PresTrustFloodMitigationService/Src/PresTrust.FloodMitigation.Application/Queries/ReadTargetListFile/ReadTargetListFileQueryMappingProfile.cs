namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryMappingProfile: Profile
{
    public ReadTargetListFileQueryMappingProfile()
    {
        CreateMap<ReadTargerListParcels, FloodParcelEntity>()
            .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.AgencyId.ToString()));
    }
}
