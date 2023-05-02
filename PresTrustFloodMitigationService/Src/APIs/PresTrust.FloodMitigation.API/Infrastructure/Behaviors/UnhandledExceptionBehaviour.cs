namespace PresTrust.FloodMitigation.API.Infrastructure.Behaviours
{
    /// <summary>
    ///     MediatR pipeline behavior to handle any unhandled exception.
    /// </summary>
    /// <typeparam name="TRequest">The request object passed in through IMediator.Send.</typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        //ILogger logger;

        /// <summary>
        ///     ctor
        /// </summary>
        public UnhandledExceptionBehaviour() //ILogger logger
        {
            //this.logger = logger;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="request">The request object passed in through IMediator.Send.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">An async continuation for the next action in the behavior chain.</param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{typeof(TRequest).Name} --> {ex.Message}");
                throw;
            }
        }
    }
}
