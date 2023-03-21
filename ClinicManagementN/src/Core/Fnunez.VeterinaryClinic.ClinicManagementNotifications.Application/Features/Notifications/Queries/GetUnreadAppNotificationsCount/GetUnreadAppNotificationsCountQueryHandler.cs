using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;

public class GetUnreadAppNotificationsCountQueryHandler
    : IRequestHandler<GetUnreadAppNotificationsCountQuery, GetUnreadAppNotificationsCountResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public GetUnreadAppNotificationsCountQueryHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetUnreadAppNotificationsCountResponse> Handle(
        GetUnreadAppNotificationsCountQuery query,
        CancellationToken cancellationToken)
    {
        GetUnreadAppNotificationsCountRequest request = query
            .GetUnreadAppNotificationsCountRequest;

        var response = new GetUnreadAppNotificationsCountResponse(
            request.CorrelationId);

        var specification = new UnreadAppNotificationSpecification(
            _currentUserService.UserId);

        int unreadAppNotificationsCount = await _unitOfWork
            .ReadRepository<AppNotification>()
            .CountAsync(specification, cancellationToken);

        response.Count = unreadAppNotificationsCount;

        return response;
    }
}