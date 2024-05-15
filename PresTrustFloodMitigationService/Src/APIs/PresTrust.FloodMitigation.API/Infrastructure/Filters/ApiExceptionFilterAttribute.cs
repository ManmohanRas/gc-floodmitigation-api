using PresTrust.FloodMitigation.Domain.Entities;

namespace PresTrust.FloodMitigation.API.Infrastructure.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly IWebHostEnvironment env;

    public ApiExceptionFilterAttribute(IWebHostEnvironment env)
    {
        this.env = env;

        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ApiModelValidationException), HandleValidationException },
                { typeof(EntityNotFoundException), HandleNotFoundException },
                { typeof(EntityAlreadyExistsException), HandleAlreadyExistsException },
                { typeof(InvalidCredentialsException), HandleInvalidCredentialsException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        ErrorDetailsEntity details = new ErrorDetailsEntity()
        {
            Title = "An error occurred while processing your request.",
            DeveloperMessage = env.IsDevelopment() ? context.Exception.Message : null
            //DeveloperMessage = context.Exception.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        ApiModelValidationException exception = context.Exception as ApiModelValidationException;

        ErrorDetailsEntity details = new ErrorDetailsEntity()
        {
            Title = exception.Message,
            Errors = exception.Errors ?? new string[] { }
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        EntityNotFoundException exception = context.Exception as EntityNotFoundException;

        ErrorDetailsEntity details = new ErrorDetailsEntity()
        {
            Title = exception.Message ?? "The specified resource was not found.",
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleAlreadyExistsException(ExceptionContext context)
    {
        EntityAlreadyExistsException exception = context.Exception as EntityAlreadyExistsException;

        ErrorDetailsEntity details = new ErrorDetailsEntity()
        {
            Title = exception.Message ?? "The specified resource already exists.",
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidCredentialsException(ExceptionContext context)
    {
        InvalidCredentialsException exception = context.Exception as InvalidCredentialsException;

        ErrorDetailsEntity details = new ErrorDetailsEntity()
        {
            Title = exception.Message ?? "User is not authorized to access the resource and/or to perform an operation .",
        };

        context.Result = new UnauthorizedObjectResult(details);

        context.ExceptionHandled = true;
    }
}
