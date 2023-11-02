namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelSurveyQueryHandler : BaseHandler, IRequestHandler<GetParcelSurveyQuery, GetParcelSurveyQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelSurveyRepository repoParcelSurvey;

    public GetParcelSurveyQueryHandler(
        IMapper mapper
       , IApplicationRepository repoApplication
       , IParcelSurveyRepository repoParcelSurvey
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcelSurvey = repoParcelSurvey;
    }
    public async Task<GetParcelSurveyQueryViewModel> Handle(GetParcelSurveyQuery request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get parcel Survey
        var parcelSurvey = await this.repoParcelSurvey.GetSurveyAsync(request.ApplicationId, request.PamsPin);
        parcelSurvey = parcelSurvey ?? new FloodParcelSurveyEntity()
        {
            ApplicationId = application.Id
        };

        var result = mapper.Map<FloodParcelSurveyEntity, GetParcelSurveyQueryViewModel>(parcelSurvey);

        return result;
    }

}
