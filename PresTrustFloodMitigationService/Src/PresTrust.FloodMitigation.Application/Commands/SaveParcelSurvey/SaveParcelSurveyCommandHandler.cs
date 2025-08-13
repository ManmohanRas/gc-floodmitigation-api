namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelSurveyCommandHandler : IRequestHandler<SaveParcelSurveyCommand, int>
{
    private IMapper mapper;
    private IParcelSurveyRepository repoParcelSurvey;
    private readonly IPresTrustUserContext userContext;

    public SaveParcelSurveyCommandHandler(
        IMapper mapper
       , IParcelSurveyRepository repoParcelSurvey,
        IPresTrustUserContext userContext

        )
    {
        this.mapper = mapper;
        this.repoParcelSurvey = repoParcelSurvey;
        this.userContext = userContext;

    }

    public async Task<int> Handle(SaveParcelSurveyCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        var reqParcelSurvey = mapper.Map<SaveParcelSurveyCommand, FloodParcelSurveyEntity>(request);

        reqParcelSurvey = await repoParcelSurvey.SaveAsync(reqParcelSurvey);

        return reqParcelSurvey.Id;
    }
}
