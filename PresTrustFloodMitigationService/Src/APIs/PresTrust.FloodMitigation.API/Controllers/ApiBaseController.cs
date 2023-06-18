namespace PresTrust.FloodMitigation.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiBaseController(IMediator mediator)
    {
        _mediator = mediator ?? throw new NullReferenceException();
    }


    /// <summary>
    /// Use for Command
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// Use for Query
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
    {
        return await _mediator.Send(query);
    }
    /// <summary>
    /// Use for check the response data,
    /// if the response is null then it will return the Not Found status code of HTTP request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    protected ActionResult<T> Single<T>(T data)
    {
        if (data == null) return NotFound();
        return Ok(data);
    }
}
