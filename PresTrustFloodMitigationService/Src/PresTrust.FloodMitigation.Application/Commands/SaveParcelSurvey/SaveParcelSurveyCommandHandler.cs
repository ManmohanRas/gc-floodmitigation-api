namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelSurveyCommandHandler : IRequestHandler<SaveParcelSurveyCommand, int>
{
    private IMapper mapper;
    private IParcelSurveyRepository repoParcelSurvey;

    public SaveParcelSurveyCommandHandler(
        IMapper mapper
       , IParcelSurveyRepository repoParcelSurvey
        )
    {
        this.mapper = mapper;
        this.repoParcelSurvey = repoParcelSurvey;
    }

    public async Task<int> Handle(SaveParcelSurveyCommand request, CancellationToken cancellationToken)
    {
        var reqParcelSurvey = mapper.Map<SaveParcelSurveyCommand, FloodParcelSurveyEntity>(request);

        reqParcelSurvey = await repoParcelSurvey.SaveAsync(reqParcelSurvey);

        return reqParcelSurvey.Id;
    }
}
