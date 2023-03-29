using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate;

public class Notification : BaseEntity<int>, IAggregateRoot
{
    public Guid CorrelationId { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public NotificationEvent NotificationEvent { get; private set; }
    public string? Payload { get; private set; }
    public string? TriggeredByUserId { get; private set; }

    #region Navigations
    public ApplicationUser TriggeredByUser { get; set; } = null!;
    #endregion

    public Notification()
    {
    }

    public Notification(
        Guid correlationId,
        DateTimeOffset createdOn,
        NotificationEvent notificationEvent,
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
        NotificationEvent = notificationEvent;
        Payload = payload;
        TriggeredByUserId = triggeredByUserId;
    }

    public Notification(
        int id,
        Guid correlationId,
        DateTimeOffset createdOn,
        NotificationEvent notificationEvent,
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
        NotificationEvent = notificationEvent;
        Payload = payload;
        TriggeredByUserId = triggeredByUserId;
    }
}