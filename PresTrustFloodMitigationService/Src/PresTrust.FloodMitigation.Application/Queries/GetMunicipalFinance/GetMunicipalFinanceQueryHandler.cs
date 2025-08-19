namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalFinanceQueryHandler : IRequestHandler<GetMunicipalFinanceQuery, GetMunicipalFinanceQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses;
    private readonly IMunicipalFinanceRepository repoFinance;
    private readonly IPresTrustUserContext userContext;

    public GetMunicipalFinanceQueryHandler(
               IMapper mapper,
               IPresTrustUserContext userContext,
               IMunicipalTrustFundPermittedUsesRepository repoMunicipalTrustFundPermittedUses,
               IMunicipalFinanceRepository repoFinance
        )
    {
        this.repoMunicipalTrustFundPermittedUses = repoMunicipalTrustFundPermittedUses;
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFinance = repoFinance;
    }
    public async Task<GetMunicipalFinanceQueryViewModel> Handle(GetMunicipalFinanceQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        var municipalTrustFundPermittedUses = await repoMunicipalTrustFundPermittedUses.GetMunicipalTrustFundPermittedUses(request.AgencyId);
        var municipalFinances = await repoFinance.GetMunicipalFinanceDetails(request.AgencyId);

        municipalTrustFundPermittedUses = municipalTrustFundPermittedUses ?? new FloodMunicipalTrustFundPermittedUsesEntity() { AgencyId = request.AgencyId };

        var result = mapper.Map<FloodMunicipalTrustFundPermittedUsesEntity, GetMunicipalFinanceQueryViewModel>(municipalTrustFundPermittedUses);
        if (municipalFinances.Count() > 0)
        {
            result.MunicipalFinances = municipalFinances;
        }

        return result;
    }
}
