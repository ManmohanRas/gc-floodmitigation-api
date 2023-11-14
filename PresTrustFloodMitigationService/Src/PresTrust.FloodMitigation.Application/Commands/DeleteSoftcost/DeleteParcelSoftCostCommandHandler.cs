namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteParcelSoftCostCommandHandler : BaseHandler, IRequestHandler<DeleteParcelSoftCostCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftCostRepository repoSoftCost;

    public DeleteParcelSoftCostCommandHandler
        (
            IMapper mapper,
            IApplicationRepository repoApplication,
            ISoftCostRepository repoSoftCost
        ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoSoftCost = repoSoftCost;
    }
    public async Task<bool> Handle(DeleteParcelSoftCostCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodParcelSoftCostEntity
        var reqSoftCost = mapper.Map<DeleteParcelSoftCostCommand, FloodParcelSoftCostEntity>(request);

        await repoSoftCost.DeleteAsync(reqSoftCost);

        return true;
    }
}
