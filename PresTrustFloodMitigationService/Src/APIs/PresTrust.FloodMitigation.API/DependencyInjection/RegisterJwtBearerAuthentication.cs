namespace PresTrust.FloodMitigation.API.DependencyInjection
{
    public class RegisterJwtBearerAuthentication : IDependencyInjectionService
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = config[AppSettingKeys.SECURITY_TOKEN_SERVICE_URL];
                o.Audience = config[AppSettingKeys.IDENTITY_SERVER_AUTHENTICATION_OPTION_API_NAME];
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                { ValidateAudience = false };
                o.SaveToken = true;
            });
            services.AddAuthorization(options =>
            {
                // policy for county level write access
                options.AddPolicy("RequireCountyWriteAccess", policy =>
                        policy.RequireRole(IdentityRoles.SYSTEM_ADMIN, IdentityRoles.FLOOD_PROGRAM_ADMIN, IdentityRoles.FLOOD_PROGRAM_EDITOR));

                // policy for county's admin access
                options.AddPolicy("RequireCountyAdminAccess", policy =>
                policy.RequireRole(IdentityRoles.SYSTEM_ADMIN, IdentityRoles.FLOOD_PROGRAM_ADMIN));

                // policy for county level read access
                options.AddPolicy("RequireCountyReadAccess", policy =>
                policy.RequireRole(IdentityRoles.SYSTEM_ADMIN, IdentityRoles.FLOOD_PROGRAM_ADMIN, IdentityRoles.FLOOD_PROGRAM_EDITOR, IdentityRoles.FLOOD_PROGRAM_READONLY));

                // policy for county committee review access
                options.AddPolicy("RequireCountyCommitteeReviewAccess", policy =>
                        policy.RequireRole(IdentityRoles.SYSTEM_ADMIN, IdentityRoles.FLOOD_PROGRAM_COMMITTEE));
                
            });
        }
    }
}
