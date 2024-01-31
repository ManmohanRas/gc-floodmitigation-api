namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelAuditDialogQueryHandler: IRequestHandler<GetParcelAuditDialogQuery, IEnumerable<GetParcelAuditDialogQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IParcelAuditDialog repoDialog;
    public GetParcelAuditDialogQueryHandler(
          IMapper mapper,
          IParcelAuditDialog repoDialog)
    {
        this.mapper = mapper;
        this.repoDialog = repoDialog;
    }

    public async Task<IEnumerable<GetParcelAuditDialogQueryViewModel>> Handle(GetParcelAuditDialogQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodParcelAuditDialogEntity> results = default;

        results = await this.repoDialog.GetParcelAuditDialogAsync(request.AgencyId);

        var dialog = mapper.Map < IEnumerable < FloodParcelAuditDialogEntity>, IEnumerable<GetParcelAuditDialogQueryViewModel>>(results);

        return dialog;
    }
}
