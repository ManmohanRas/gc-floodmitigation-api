namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelSurveyQueryHandler : BaseHandler, IRequestHandler<GetParcelSurveyQuery, GetParcelSurveyQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelSurveyRepository repoParcelSurvey;
    private readonly IPresTrustUserContext userContext;

    public GetParcelSurveyQueryHandler(
        IMapper mapper
        , IPresTrustUserContext userContext
       , IApplicationRepository repoApplication
       , IParcelSurveyRepository repoParcelSurvey
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplication = repoApplication;
        this.repoParcelSurvey = repoParcelSurvey;
    }
    public async Task<GetParcelSurveyQueryViewModel> Handle(GetParcelSurveyQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get parcel Survey
        var parcelSurvey = await this.repoParcelSurvey.GetSurveyAsync(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodParcelSurveyEntity, GetParcelSurveyQueryViewModel>(parcelSurvey);

        return result;
    }

}
