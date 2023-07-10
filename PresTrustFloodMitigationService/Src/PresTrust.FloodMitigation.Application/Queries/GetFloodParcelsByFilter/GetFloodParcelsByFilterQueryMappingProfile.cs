namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQueryMappingProfile : Profile
{
    public GetFloodParcelsByFilterQueryMappingProfile()
    {
        CreateMap<FloodParcelEntity, GetFloodParcelsByFilterQueryViewModel>();
    }
}
