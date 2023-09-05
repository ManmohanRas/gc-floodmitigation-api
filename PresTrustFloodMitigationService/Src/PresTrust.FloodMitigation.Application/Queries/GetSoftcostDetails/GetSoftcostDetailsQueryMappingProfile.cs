namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftcostDetailsQueryMappingProfile : Profile
{
    public GetSoftcostDetailsQueryMappingProfile()
    {
        CreateMap<FloodParcelSoftcostEntity, FloodParcelSoftcostViewModel>();
    }
}
