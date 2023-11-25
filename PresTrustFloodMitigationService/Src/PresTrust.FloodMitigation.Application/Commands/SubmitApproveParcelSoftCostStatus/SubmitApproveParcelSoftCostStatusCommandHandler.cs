namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApproveParcelSoftCostStatusCommandHandler : IRequestHandler<SubmitApproveParcelSoftCostStatusCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;

    public SubmitApproveParcelSoftCostStatusCommandHandler(
        IMapper mapper,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationRepository repoApplication
        )
    {
        this.mapper = mapper;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplication = repoApplication;
    }
    public async Task<bool> Handle(SubmitApproveParcelSoftCostStatusCommand request, CancellationToken cancellationToken)
    {
        var reqParcelStatus = mapper.Map<SubmitApproveParcelSoftCostStatusCommand, FloodApplicationParcelEntity>(request);

        await repoApplicationParcel.UpdateApplicationParcelSoftCostStatus(reqParcelStatus);

        return true;
    }
}
