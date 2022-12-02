using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest
    : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            string requestName = typeof(TRequest).Name;

            var validatedErrors = ex.Errors
                .Select(x => $"{x.Key} - {string.Join(',', x.Value)}");

            _logger.LogError(
                ex,
                "ClinicManagement Validation Exception for Request: {Name} {@Errors}",
                requestName,
                validatedErrors
            );

            throw;
        }
        catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;
            string serializedRequest = JsonSerializer.Serialize(request);

            _logger.LogError(
                ex,
                "ClinicManagement Unhandled Exception for Request: {Name} {@Request}",
                requestName,
                serializedRequest
            );

            throw;
        }
    }
}