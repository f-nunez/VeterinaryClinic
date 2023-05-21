using System.Text.Json;
using Contracts.SchedulingEmailSender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine;

public class EmailEngineService : IEmailEngineService
{
    private readonly IEmailCompositionFactory _emailCompositionFactory;
    private readonly IEmailRequestFactory _emailRequestFactory;
    private readonly ILogger<EmailEngineService> _logger;
    private readonly IPayloadFactory _payloadFactory;
    private readonly IServiceBus _serviceBus;
    private readonly IUnitOfWork _unitOfWork;

    public EmailEngineService(
        IEmailCompositionFactory emailCompositionFactory,
        IEmailRequestFactory emailRequestFactory,
        ILogger<EmailEngineService> logger,
        IPayloadFactory payloadFactory,
        IServiceBus serviceBus,
        IUnitOfWork unitOfWork)
    {
        _emailCompositionFactory = emailCompositionFactory;
        _emailRequestFactory = emailRequestFactory;
        _logger = logger;
        _payloadFactory = payloadFactory;
        _serviceBus = serviceBus;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAndSendAsync(
        string emailEventString,
        string serializedEmailRequest,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(emailEventString))
            throw new ArgumentException(
                $"{nameof(emailEventString)} is empty.");

        if (string.IsNullOrEmpty(serializedEmailRequest))
            throw new ArgumentException(
                $"{nameof(serializedEmailRequest)} is empty.");

        EmailEvent emailEvent = GetEmailEvent(emailEventString);

        BaseEmailRequest emailRequest = _emailRequestFactory
            .GetEmailRequest(emailEvent, serializedEmailRequest);

        BasePayload payload = _payloadFactory
            .GetPayload(emailEvent, emailRequest);

        await CreateEmailAsync(
           emailEvent, emailRequest, payload, cancellationToken);

        EmailComposition emailComposition = await _emailCompositionFactory
            .GetEmailCompositionAsync(emailEvent, payload);

        try
        {
            var correlationId = Guid.NewGuid();

            var emailContract = new EmailCompositionSchedulingEmailSenderContract
            {
                CausationId = correlationId,
                CorrelationId = correlationId,
                Id = correlationId,
                OccurredOn = DateTimeOffset.UtcNow,
                SerializedEmailComposition = JsonSerializer.Serialize(emailComposition)
            };

            await _serviceBus.PublishAsync(emailContract, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }

    private async Task CreateEmailAsync(
        EmailEvent emailEvent,
        BaseEmailRequest emailRequest,
        BasePayload payload,
        CancellationToken cancellationToken)
    {
        var email = new Email(
            emailRequest.CorrelationId,
            emailRequest.SendTo,
            DateTimeOffset.UtcNow,
            emailEvent,
            JsonSerializer.Serialize((object)payload),
            emailRequest.TriggeredByUserId
        );

        await _unitOfWork
            .Repository<Email>()
            .AddAsync(email, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private EmailEvent GetEmailEvent(string emailEventString)
    {
        bool isParsedEmailEvent = Enum.TryParse(
            emailEventString, out EmailEvent emailEvent);

        if (isParsedEmailEvent)
            return emailEvent;

        throw new ArgumentException(
            $"{nameof(emailEventString)} not found with value: {emailEventString}");
    }
}