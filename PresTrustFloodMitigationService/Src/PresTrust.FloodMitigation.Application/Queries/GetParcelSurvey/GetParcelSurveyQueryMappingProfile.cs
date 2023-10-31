namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelSurveyQueryMappingProfile : Profile
{
    public GetParcelSurveyQueryMappingProfile()
    {
        CreateMap<FloodParcelSurveyEntity, GetParcelSurveyQueryViewModel>();
    }
}
