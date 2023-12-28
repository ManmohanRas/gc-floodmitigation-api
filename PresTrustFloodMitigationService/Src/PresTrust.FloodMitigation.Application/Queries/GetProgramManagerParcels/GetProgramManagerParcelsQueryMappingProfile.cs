namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramManagerParcelsQueryMappingProfile : Profile
{
    public GetProgramManagerParcelsQueryMappingProfile()
    {
        CreateMap<FloodProgramManagerParcelsEntity, GetProgramManagerParcelsQueryViewModel>();
    }
}
