namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSignatoryQueryMappingProfile:Profile
{
    public GetSignatoryQueryMappingProfile()
    {
        CreateMap<FloodSignatoryEntity, GetSignatoryQueryViewModel>();
    }
}
