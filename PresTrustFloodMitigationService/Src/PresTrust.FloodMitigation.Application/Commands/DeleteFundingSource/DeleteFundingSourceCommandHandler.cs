namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class DeleteFundingSourceCommandHandler: IRequestHandler<DeleteFundingSourceCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IFundingSourceRepoitory repoFundingSource;

    public DeleteFundingSourceCommandHandler(
        IMapper mapper,
        IFundingSourceRepoitory repoFundingSource
        )
    {
        this.mapper = mapper;
        this.repoFundingSource = repoFundingSource;
    }
    public async Task<bool> Handle(DeleteFundingSourceCommand request, CancellationToken cancellationToken)
    {
        // map command object to the FloodFundingSourceEntity
        var reqFundingSorce = mapper.Map<DeleteFundingSourceCommand, FloodFundingSourceEntity>(request);
        // delete funding source
        await repoFundingSource.DeleteAsync(reqFundingSorce);

        return true;
    }
}
