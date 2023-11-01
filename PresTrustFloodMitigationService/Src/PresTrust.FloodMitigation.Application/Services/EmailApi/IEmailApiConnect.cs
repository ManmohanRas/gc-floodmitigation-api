namespace PresTrust.FloodMitigation.Application.Services.EmailApi;

public interface IEmailApiConnect
{
    Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, JsonContent content);
}

public interface IReminderEmailApiConnect
{
    Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, JsonContent content);
}