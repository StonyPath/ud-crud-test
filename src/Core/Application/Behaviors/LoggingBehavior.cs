using MediatR;
using Serilog;

namespace Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Log.Information("Handling {RequestName}", typeof(TRequest).Name);
        var response = await next();
        Log.Information("Handled {RequestName}", typeof(TRequest).Name);
        return response;
    }
}
