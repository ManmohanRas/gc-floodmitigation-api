namespace PresTrust.FloodMitigation.API.DependencyInjection
{
    public class RegisterCors : IDependencyInjectionService
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            string[] allowedOrigins = config.GetSection(FloodMitigationDomainConstants.AppSettingKeys.CORS_POLICIES_SECTION).Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(FloodMitigationDomainConstants.AppSettingKeys.CORS_POLICY_NAME, builder =>
                {
                    builder.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
        }
    }
}
