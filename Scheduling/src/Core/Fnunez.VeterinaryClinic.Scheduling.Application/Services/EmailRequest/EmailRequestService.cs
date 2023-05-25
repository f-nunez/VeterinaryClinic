using System.Text.Json;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Contracts.Scheduling;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;

public class EmailRequestService : IEmailRequestService
{
    private readonly IServiceBus _serviceBus;

    public EmailRequestService(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task CreateAndSendAsync(
        IEmailRequestFactory factory,
        CancellationToken cancellationToken)
    {
        var emailRequest = factory.CreateEmailRequest();

        var message = new EmailRequestSchedulingContract
        {
            CausationId = emailRequest.CorrelationId,
            CorrelationId = emailRequest.CorrelationId,
            EmailEvent = factory.GetEmailEvent(),
            Id = emailRequest.CorrelationId,
            OccurredOn = DateTimeOffset.UtcNow,
            SerializedEmailRequest = JsonSerializer.Serialize((object)emailRequest)
        };

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}