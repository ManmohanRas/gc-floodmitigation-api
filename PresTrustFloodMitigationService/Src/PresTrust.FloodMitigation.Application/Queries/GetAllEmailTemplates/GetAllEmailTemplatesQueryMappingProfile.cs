namespace PresTrust.FloodMitigation.Application.Queries;

public class GetAllEmailTemplatesQueryMappingProfile: Profile
{
    public GetAllEmailTemplatesQueryMappingProfile() 
    { 
        CreateMap<FloodEmailTemplateEntity, GetAllEmailTemplatesQueryViewModel>();
    }
}
