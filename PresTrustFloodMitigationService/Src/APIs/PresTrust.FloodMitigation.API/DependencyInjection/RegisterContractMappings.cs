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
        services.AddTransient<ISoftCostRepository, SoftCostRepository>();
        services.AddTransient<IFinanceRepository, FinanceRepository>();
        services.AddTransient<IFundingSourceRepoitory, FundingSourceRepoitory>();
        services.AddTransient<IFinanceLineItemRepository, FinanceLineItemRepository>();
        services.AddTransient<IApplicationFundingAgencyRepository, ApplicationFundingAgencyRepository>();
        services.AddTransient<ITechDetailsRepository, TechDetailsRepository>();
        services.AddTransient<IParcelRepository, ParcelRepository>();
        services.AddTransient<ICommentPropRepository, CommentPropRepository>();
        services.AddTransient<IBrokenRuleRepository, BrokenRuleRepository>();
        services.AddTransient<IContactRepository, ContactRepository>();
        services.AddTransient<IPropReleaseOfFundsRepository, PropReleaseOfFundsRepository>();
        services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddTransient<IEmailManager, EmailManager>();
        services.AddTransient<IParcelPropertyRepository, ParcelPropertyRepository>();
        services.AddTransient<IApplicationReleaseOfFundsRepository, ApplicationReleaseOfFundsRepository>();
        services.AddTransient<IPropertyDocumentRepository, PropertyDocumentRepository>();
        services.AddTransient<IApplicationDetailsRepository, ApplicationDetailsRepository>();
        services.AddTransient<IPropertyAdminDetailsRepository, PropertyAdminDetailsRepository>();
        services.AddTransient<IReminderEmailManager, ReminderEmailManager>();
        services.AddTransient<IGrantExpirationReminder, GrantExpirationReminder>();
        services.AddTransient<IParcelSurveyRepository, ParcelSurveyRepository>();
        services.AddTransient<IParcelTrackingRepository, ParcelTrackingRepository>();
        services.AddTransient<IPropertyBrokenRuleRepository, PropertyBrokenRulesRepository>();
        services.AddTransient<IFlapModuleRepository, FlapModuleRepository>();

        services.AddHttpContextAccessor();
        services.AddSession();

    }
}                                                                               
