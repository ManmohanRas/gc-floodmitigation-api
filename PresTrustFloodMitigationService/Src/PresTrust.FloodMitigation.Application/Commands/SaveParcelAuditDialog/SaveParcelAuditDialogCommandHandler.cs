namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelAuditDialogCommandHandler: IRequestHandler<SaveParcelAuditDialogCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IParcelAuditDialog repoDialog;

    public SaveParcelAuditDialogCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IParcelAuditDialog repoDialog
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoDialog = repoDialog;
    }

    public async Task<int> Handle(SaveParcelAuditDialogCommand request, CancellationToken cancellationToken)
    {

        var repoDialog = mapper.Map<SaveParcelAuditDialogCommand, FloodParcelAuditDialogEntity>(request);


        // save dialog
        FloodParcelAuditDialogEntity dialog = default;
        repoDialog.LastUpdatedBy = this.userContext.Name;
        dialog = await this.repoDialog.SaveParcelAuditDialogAsync(repoDialog);

        return dialog.Id;
    }
}
