namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommandMappingProfile : Profile
{
    public SavePropReleaseOfFundsCommandMappingProfile()
    {
        CreateMap<SavePropReleaseOfFundsCommand, FloodPropReleaseOfFundsEntity>();
    }
}
