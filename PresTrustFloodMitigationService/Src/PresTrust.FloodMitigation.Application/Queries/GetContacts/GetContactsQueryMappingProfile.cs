namespace PresTrust.FloodMitigation.Application.Queries;

public class GetContactsQueryMappingProfile : Profile
{
    public GetContactsQueryMappingProfile()
    {
        CreateMap<FloodContactEntity, GetContactsQueryViewModel>();
    }
}
