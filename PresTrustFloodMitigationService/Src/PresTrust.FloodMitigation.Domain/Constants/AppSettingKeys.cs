namespace PresTrust.FloodMitigation.Domain.Constants;

public partial class FloodMitigationDomainConstants
{
    public static class AppSettingKeys
    {
        /// <summary>
        /// constant for the Logging Section
        /// </summary>
        public const string LOGGING_SECTION = "Logging";
        /// <summary>
        /// constant for the ConnectionString Section
        /// </summary>
        public const string PRESTRUST_SQL_DB_CONNECTION_STRING_SECTION = "PresTrustDbConnectionStrings";
        /// <summary>
        /// constant for the API Resource Base Url (external apis) Section
        /// </summary>
        public const string API_RESOURCE_BASE_URL_SECTION = "ApiResourceBaseUrls";
        /// <summary>
        /// constant for the HttpClientPolicies Section
        /// </summary>
        public const string HTTP_CLIENT_POLICIES_SECTION = "HttpClientPolicies";
        /// <summary>
        /// constant for the CORS Policies Section
        /// </summary>
        public const string CORS_POLICIES_SECTION = "CorsPolicy:AllowedOrigins";
        /// <summary>
        /// constant for the CORS Policies Section
        /// </summary>
        public const string CORS_POLICY_NAME = "AllowedOrigins";
        /// <summary>
        /// constant for the Security Token Service's URL
        /// </summary>
        public const string SECURITY_TOKEN_SERVICE_URL = "SecurityTokenServiceUrl";
        /// <summary>
        /// constant for the System Parameters Section
        /// </summary>
        public const string SYSTEM_PARAMETER_SECTION = "SystemParameters";
        /// <summary>
        /// constant for the Identity Server Authentication Option API Name
        /// </summary>
        public const string IDENTITY_SERVER_AUTHENTICATION_OPTION_API_NAME = "IdentityServerAuthenticationOptionApiName";
    }
}
