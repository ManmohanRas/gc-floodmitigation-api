namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveAnnualFundingDetailsCommandHandler : IRequestHandler<SaveAnnualFundingDetailsCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IAnnualFundingAmountsRepository repoAnnualFunding;

    public SaveAnnualFundingDetailsCommandHandler
      (
      IMapper mapper,
      IPresTrustUserContext userContext,
      IOptions<SystemParameterConfiguration> systemParamOptions,
      IAnnualFundingAmountsRepository repoAnnualFunding
      )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoAnnualFunding = repoAnnualFunding;
    }

    public async Task<bool> Handle(SaveAnnualFundingDetailsCommand request, CancellationToken cancellationToken)
    {

        var reqDetails = mapper.Map<SaveAnnualFundingDetailsCommand, FloodAnnualFundingEntity>(request);
        await repoAnnualFunding.SaveAsync(reqDetails);
        return true;
    }

}
