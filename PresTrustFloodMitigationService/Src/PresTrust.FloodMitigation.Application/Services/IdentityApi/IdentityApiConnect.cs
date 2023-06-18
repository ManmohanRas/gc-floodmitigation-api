namespace PresTrust.FloodMitigation.Application.Services.IdentityApi;

public class IdentityApiConnect : IIdentityApiConnect
{
    private readonly HttpClient _httpClient;
    private readonly IPresTrustUserContext userContext;
    public IdentityApiConnect(HttpClient httpClient, IPresTrustUserContext userContext)
    {
        _httpClient = httpClient;
        this.userContext = userContext;
    }

    public async Task<TResponse> GetDataAsync<TResponse>(string endPoint)
    {
        TResponse data = default;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userContext.AccessToken);

        try
        {
            var httpResponse = await _httpClient.GetAsync(endPoint);

            httpResponse.EnsureSuccessStatusCode();

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<TResponse>(responseAsString);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound || ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            throw;
        }

        return data;
    }

    public async Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, JsonContent content)
    {
        TResponse data = default;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userContext.AccessToken);

        try
        {
            var httpResponse = await _httpClient.PostAsync(endPoint, content);

            httpResponse.EnsureSuccessStatusCode();

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<TResponse>(responseAsString);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound || ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            throw;
        }

        return data;
    }

    public async Task<TResponse> PutDataAsync<TResponse, TRequest>(string endPoint, JsonContent content)
    {
        TResponse data = default;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userContext.AccessToken);

        try
        {
            var httpResponse = await _httpClient.PutAsync(endPoint, content);

            httpResponse.EnsureSuccessStatusCode();

            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<TResponse>(responseAsString);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound || ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            throw;
        }

        return data;
    }
}
