using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class AppNotificationSpecification : BaseSpecification<AppNotification>
{
    public AppNotificationSpecification(
        GetAppNotificationsDataGridRequest request,
        string? userId)
    {
        Query
            .AsNoTracking()
            .Include(an => an.Notification)
            .ThenInclude(n => n.TriggeredByUser)
            .Where(an => an.IsActive && an.UserId == userId);

        ApplyFilterOnlyRead(request);

        ApplyFilterOnlyUnread(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterOnlyRead(
        GetAppNotificationsDataGridRequest request)
    {
        if (!request.OnlyReadFilterValue)
            return;

        Query
            .Where(an => an.ReadOn != null);
    }

    private void ApplyFilterOnlyUnread(
        GetAppNotificationsDataGridRequest request)
    {
        if (!request.OnlyUnreadFilterValue)
            return;

        Query
            .Where(an => an.ReadOn == null);
    }

    private void ApplyOrder(GetAppNotificationsDataGridRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        SetOrderBy(dataGridRequest);
    }

    private void ApplySkipAndTake(GetAppNotificationsDataGridRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<AppNotification> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "CreatedOn":
                if (sort.IsAscending)
                    return Query.OrderBy(an => an.CreatedOn);
                else
                    return Query.OrderByDescending(an => an.CreatedOn);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }
}