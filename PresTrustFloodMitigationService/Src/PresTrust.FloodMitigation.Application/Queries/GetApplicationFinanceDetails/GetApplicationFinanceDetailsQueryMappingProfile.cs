namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationFinanceDetailsQueryMappingProfile: Profile
{
    public GetApplicationFinanceDetailsQueryMappingProfile()
    {
        CreateMap<FloodApplicationFinanceEntity, GetApplicationFinanceDetailsQueryViewModel>();
        CreateMap<FloodFundingSourceEntity, FloodFundingSourceViewModel>();
        CreateMap<FloodFinanceLineItemEntity, FloodFinanceLineItemViewModel>();
    }
}
