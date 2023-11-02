namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelSurveyCommandMappingProfile : Profile
{
     public SaveParcelSurveyCommandMappingProfile()
    {
        CreateMap<SaveParcelSurveyCommand, FloodParcelSurveyEntity>();
    }
}
