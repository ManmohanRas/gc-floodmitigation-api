namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelListQueryMappingProfile : Profile
{
    public GetParcelListQueryMappingProfile()
    {
        CreateMap<FloodParcelListEntity, GetParcelListQueryViewModel>();
    }
}
