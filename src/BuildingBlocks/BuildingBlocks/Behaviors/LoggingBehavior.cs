using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            $"[START] Handle request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - RequestData={request}");

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
            logger.LogWarning(
                $"[PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken.Seconds} seconds to complete");

        logger.LogInformation($"[END] Handled {typeof(TRequest).Name} with response {response}");
        return response;
    }
}