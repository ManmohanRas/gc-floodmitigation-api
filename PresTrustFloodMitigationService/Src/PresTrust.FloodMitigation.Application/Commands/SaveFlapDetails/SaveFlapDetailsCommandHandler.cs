namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDetailsCommandHandler : IRequestHandler<SaveFlapDetailsCommand, Unit>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private IFlapModuleRepository repoFlap;

    public SaveFlapDetailsCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IFlapModuleRepository repoFlap
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoFlap = repoFlap;
    }
    public async Task<Unit> Handle(SaveFlapDetailsCommand request, CancellationToken cancellationToken)
    {
        var flap = mapper.Map<SaveFlapDetailsCommand, FloodFlapEntity>(request);
        flap.LastUpdatedBy = userContext.Email;

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoFlap.SaveFlapAsync(flap);
            if (request.FlapComments.Count() > 0) {
                await UpdateFlapComments(request.FlapComments);
            }

            scope.Complete();
        }
            return Unit.Value;
    }

    private async Task UpdateFlapComments(IEnumerable<FloodFlapCommentViewModel> flapComments)
    {
        foreach(var comment in flapComments)
        {
            var entity = mapper.Map<FloodFlapCommentViewModel, FloodFlapCommentEntity>(comment);
            entity.LastUpdatedBy = userContext.Email;

            if (comment.RowStatus.EndsWith("U", StringComparison.OrdinalIgnoreCase))
            {
                await repoFlap.SaveFlapCommentAsync(entity);
            }
            if (comment.RowStatus.EndsWith("D", StringComparison.OrdinalIgnoreCase))
            {
                await repoFlap.DeleteFlapCommentAsync(entity);
            }
        }
    }
}
