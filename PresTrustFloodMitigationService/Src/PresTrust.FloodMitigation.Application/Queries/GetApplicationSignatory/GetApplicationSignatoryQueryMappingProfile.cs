namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationSignatoryQueryMappingProfile:Profile
{
    public GetApplicationSignatoryQueryMappingProfile()
    {
        CreateMap<FloodApplicationSignatoryEntity, GetApplicationSignatoryQueryViewModel>();
    }
}
