namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelTrackingCommandHandler : BaseHandler,IRequestHandler<SaveParcelTrackingCommand, int>
{
    private IMapper mapper;
    private IParcelTrackingRepository repoParcelTracking;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelPropertyRepository repoProperty;


    public SaveParcelTrackingCommandHandler(
        IMapper mapper,
        IParcelTrackingRepository repoParcelTracking,
        IParcelPropertyRepository repoProperty,
        IApplicationRepository repoApplication
        ) : base (repoApplication :repoApplication)
    {
        this.mapper = mapper;
        this.repoParcelTracking = repoParcelTracking;
    }

    public async Task<int> Handle(SaveParcelTrackingCommand request, CancellationToken cancellationToken)
    {
        var reqParcelTracking = mapper.Map<SaveParcelTrackingCommand, FloodParcelTrackingEntity>(request);

        reqParcelTracking = await repoParcelTracking.SaveAsync(reqParcelTracking);

        return reqParcelTracking.Id;
    }
}
