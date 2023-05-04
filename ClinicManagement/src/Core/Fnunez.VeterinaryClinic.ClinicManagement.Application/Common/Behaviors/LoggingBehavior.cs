using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest
    : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string serializedRequest = JsonSerializer.Serialize(request);

        _logger.LogInformation(
            "ClinicManagement Handling Request: {Name} {@Request}",
            requestName,
            serializedRequest
        );

        TResponse response = await next();

        _logger.LogInformation(
            "ClinicManagement Handled Request: {Name}",
            requestName
        );

        return response;
    }
}