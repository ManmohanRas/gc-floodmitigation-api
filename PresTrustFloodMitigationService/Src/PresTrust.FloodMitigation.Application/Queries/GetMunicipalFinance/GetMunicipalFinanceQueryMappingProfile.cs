using PresTrust.FloodMitigation.Domain.Entities;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalFinanceQueryMappingProfile: Profile
{
    public GetMunicipalFinanceQueryMappingProfile()
    {
        CreateMap<FloodMunicipalTrustFundPermittedUsesEntity, GetMunicipalFinanceQueryViewModel>();
    }
}
