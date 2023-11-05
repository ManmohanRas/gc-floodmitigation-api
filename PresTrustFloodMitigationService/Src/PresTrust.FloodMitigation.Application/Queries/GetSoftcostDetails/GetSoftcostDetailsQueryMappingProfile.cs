namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQueryMappingProfile : Profile
{
    public GetSoftCostDetailsQueryMappingProfile()
    {
        CreateMap<FloodParcelSoftCostEntity, FloodParcelSoftCostViewModel>();
    }
}
