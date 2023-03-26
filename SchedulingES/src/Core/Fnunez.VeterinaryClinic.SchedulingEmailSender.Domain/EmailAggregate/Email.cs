using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate;

public class Email : BaseEntity<int>, IAggregateRoot
{
    public Guid CorrelationId { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public EmailEvent EmailEvent { get; private set; }
    public string? Payload { get; private set; }
    public int RetryCount { get; private set; }
    public DateTimeOffset? SentOn { get; private set; }
    public string? TriggeredByUserId { get; private set; }

    public Email()
    {
    }

    public Email(
        Guid correlationId,
        DateTimeOffset createdOn,
        EmailEvent emailEvent,
        string? payload,
        string? triggeredByUserId)
    {
        if (correlationId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(correlationId)} was empty.",
                nameof(correlationId));

        if (string.IsNullOrEmpty(payload))
            throw new ArgumentException(
                $"Required input {nameof(payload)} was empty.",
                nameof(payload));

        if (string.IsNullOrEmpty(triggeredByUserId))
            throw new ArgumentException(
                $"Required input {nameof(triggeredByUserId)} was empty.",
                nameof(triggeredByUserId));

        CorrelationId = correlationId;
        CreatedOn = createdOn;
        EmailEvent = emailEvent;
        Payload = payload;
        TriggeredByUserId = triggeredByUserId;
    }

    public Email(
        int id,
        Guid correlationId,
        DateTimeOffset createdOn,
        EmailEvent emailEvent,
        string? payload,
        string? triggeredByUserId)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

        if (correlationId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(correlationId)} was empty.",
                nameof(correlationId));

        if (string.IsNullOrEmpty(payload))
            throw new ArgumentException(
                $"Required input {nameof(payload)} was empty.",
                nameof(payload));

        if (string.IsNullOrEmpty(triggeredByUserId))
            throw new ArgumentException(
                $"Required input {nameof(triggeredByUserId)} was empty.",
                nameof(triggeredByUserId));

        Id = id;
        CorrelationId = correlationId;
        CreatedOn = createdOn;
        EmailEvent = emailEvent;
        Payload = payload;
        TriggeredByUserId = triggeredByUserId;
    }

    public void IncreaseRetryCount()
    {
        RetryCount++;
    }

    public void UpdateSentOn(DateTimeOffset sentOn)
    {
        SentOn = sentOn;
    }
}