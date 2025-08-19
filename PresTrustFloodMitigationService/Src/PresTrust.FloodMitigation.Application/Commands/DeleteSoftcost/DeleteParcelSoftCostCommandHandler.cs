namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteParcelSoftCostCommandHandler : BaseHandler, IRequestHandler<DeleteParcelSoftCostCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftCostRepository repoSoftCost;
    private readonly IPresTrustUserContext userContext;

    public DeleteParcelSoftCostCommandHandler
        (
            IMapper mapper,
            IApplicationRepository repoApplication,
            ISoftCostRepository repoSoftCost,
            IPresTrustUserContext userContext
        ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoSoftCost = repoSoftCost;
        this.userContext = userContext;
    }
    public async Task<bool> Handle(DeleteParcelSoftCostCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodParcelSoftCostEntity
        var reqSoftCost = mapper.Map<DeleteParcelSoftCostCommand, FloodParcelSoftCostEntity>(request);

        await repoSoftCost.DeleteAsync(reqSoftCost);

        return true;
    }
}
