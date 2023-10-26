namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQueryMappingProfile : Profile
{
    public GetApplicationReleaseOfFundsQueryMappingProfile()
    {
        CreateMap<FloodApplicationReleaseOfFundsEntity, GetApplicationReleaseOfFundsQueryViewModel>();
        CreateMap<FloodPropReleaseOfFundsEntity, FloodParcelReleaseOfFundsViewModel>()
            .ForMember(dest => dest.HardCostPaymentStatus, opt => opt.MapFrom(src => src.HardCostPaymentStatus.ToString()))
            .ForMember(dest => dest.SoftCostPaymentStatus, opt => opt.MapFrom(src => src.SoftCostPaymentStatus.ToString()))
            .ForMember(dest => dest.HardCostPaymentType, opt => opt.MapFrom(src => src.HardCostPaymentType.ToString()))
            .ForMember(dest => dest.SoftCostPaymentType, opt => opt.MapFrom(src => src.SoftCostPaymentType.ToString()));
    }
}
