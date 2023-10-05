using PresTrust.FloodMitigation.Application;

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
        services.AddTransient<IApplicationSignatoryRepository, ApplicationSignatoryRepository>();
        services.AddTransient<IApplicationFeedbackRepository, ApplicationFeedbackRepository>();
        services.AddTransient<IApplicationCommentRepository, ApplicationCommentRepository>();
        services.AddTransient<IApplicationOverviewRepository, ApplicationOverviewRepository>();
        services.AddTransient<IApplicationDocumentRepository, ApplicationDocumentRepository>();
        services.AddTransient<IFeedbackPropRepository, FeedbackPropRepository>();
        services.AddTransient<IParcelFinanceRepository, ParcelFinanceRepository>();
        services.AddTransient<ISoftcostRepository, SoftcostRepository>();
        services.AddTransient<IFinanceRepository, FinanceRepository>();
        services.AddTransient<IFundingSourceRepoitory, FundingSourceRepoitory>();
        services.AddTransient<IFinanceLineItemRepository, FinanceLineItemRepository>();
        services.AddTransient<IApplicationFundingAgencyRepository, ApplicationFundingAgencyRepository>();
        services.AddTransient<ITechDetailsRepository, TechDetailsRepository>();
        services.AddTransient<IFloodParcelRepository, FloodParcelRepository>();
        services.AddTransient<ICommentPropRepository, CommentPropRepository>();
        services.AddTransient<IBrokenRuleRepository, BrokenRuleRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEmailManager, EmailManager>();
        services.AddHttpContextAccessor();
    }
}                                                                               
