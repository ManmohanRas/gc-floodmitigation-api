namespace PresTrust.FloodMitigation.API.DependencyInjection;

public class RegisterContractMappings : IDependencyInjectionService
{
    public void Register(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IPresTrustUserContext, PresTrustUserContext>();
        services.AddTransient<ICoreRepository, CoreRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<IApplicationParcelRepository, ApplicationParcelRepository>();
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient<ISignatoryRepository, SignatoryRepository>();
        services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IOverviewDetailsRepository, OverviewDetailsRepository>();
        services.AddTransient<IDocumentRepository, DocumentRepository>();
        services.AddTransient<IFeedbackPropRepository, FeedbackPropRepository>();
        services.AddTransient<IParcelFinanceRepository, ParcelFinanceRepository>();
        services.AddTransient<ISoftcostRepository, SoftcostRepository>();
        services.AddTransient<IFinanceRepository, FinanceRepository>();
        services.AddTransient<IFundingSourceRepoitory, FundingSourceRepoitory>();
        services.AddTransient<IFinanceLineItemRepository, FinanceLineItemRepository>();
        services.AddTransient<IFundingAgencyRepository, FundingAgencyRepository>();
        services.AddTransient<ITechDetailsRepository, TechDetailsRepository>();
        services.AddTransient<IFloodParcelRepository, FloodParcelRepository>();
        services.AddTransient<ICommentPropRepository, CommentPropRepository>();
        services.AddTransient<IBrokenRuleRepository, BrokenRuleRepository>();
        services.AddHttpContextAccessor();
    }
}                                                                               
