namespace PresTrust.FloodMitigation.Application.Services.IdentityApi
{
    public interface IIdentityApiConnect
    {
        Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, JsonContent content);
        Task<TResponse> PutDataAsync<TResponse, TRequest>(string endPoint, JsonContent content);
        Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    }
}
