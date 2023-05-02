namespace PresTrust.FloodMitigation.API.DependencyInjection
{
    public class RegisterApiResources : IDependencyInjectionService
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            var policyConfigs = new HttpClientPolicyConfiguration();
            config.Bind(FloodMitigationDomainConstants.AppSettingKeys.HTTP_CLIENT_POLICIES_SECTION, policyConfigs);

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(policyConfigs.RetryTimeoutInSeconds));

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => r.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(policyConfigs.RetryCount, _ => TimeSpan.FromMilliseconds(policyConfigs.RetryDelayInMs));

            var circuitBreakerPolicy = HttpPolicyExtensions
               .HandleTransientHttpError()
               .CircuitBreakerAsync(policyConfigs.MaxAttemptBeforeBreak, TimeSpan.FromSeconds(policyConfigs.BreakDurationInSeconds));

            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            services.AddHttpClient<IIdentityApiConnect, IdentityApiConnect>(client =>
            {
                client.BaseAddress = new Uri(config["ApiResourceBaseUrls:IdentityApi"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(FloodMitigationDomainConstants.HttpContentMediaTypes.JSON));
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(policyConfigs.HandlerTimeoutInMinutes))
            .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
            .AddPolicyHandler(timeoutPolicy);
            //.AddPolicyHandler(circuitBreakerPolicy);

            //EmailApiConnect 
            services.AddHttpClient<IEmailApiConnect, EmailApiConnect>(client =>
            {
                client.BaseAddress = new Uri(config["ApiResourceBaseUrls:EmailApi"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(FloodMitigationDomainConstants.HttpContentMediaTypes.JSON));
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(policyConfigs.HandlerTimeoutInMinutes))
            .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOpPolicy)
            .AddPolicyHandler(timeoutPolicy);
            //.AddPolicyHandler(circuitBreakerPolicy);
        }
    }
}