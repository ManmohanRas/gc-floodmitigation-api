namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDetailsQueryMappingProfile : Profile
{
    public GetPropertyDetailsQueryMappingProfile()
    {
        CreateMap<FloodParcelEntity, GetPropertyDetailsQueryViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
