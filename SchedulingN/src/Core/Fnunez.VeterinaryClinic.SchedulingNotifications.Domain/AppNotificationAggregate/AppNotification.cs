using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;

public class AppNotification : BaseEntity<Guid>, IAggregateRoot
{
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset? DeletedOn { get; private set; }
    public DateTimeOffset? ReadOn { get; private set; }
    public int NotificationId { get; private set; }
    public string? UserId { get; private set; }

    #region Navigations
    public Notification Notification { get; set; } = null!;
    #endregion

    public AppNotification()
    {
    }

    public AppNotification(
        Guid id,
        DateTimeOffset createdOn,
        int notificationId,
        string? userId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} was empty.",
                nameof(id));

        if (notificationId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(notificationId)} cannot be zero or negative.",
                nameof(notificationId));

        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException(
                $"Required input {nameof(userId)} was empty.",
                nameof(userId));

        Id = id;
        CreatedOn = createdOn;
        NotificationId = notificationId;
        UserId = userId;
    }

    public void UpdateDeletedOn(DateTimeOffset deletedOn)
    {
        DeletedOn = deletedOn;
    }

    public void UpdateReadOn(DateTimeOffset readOn)
    {
        ReadOn = readOn;
    }
}