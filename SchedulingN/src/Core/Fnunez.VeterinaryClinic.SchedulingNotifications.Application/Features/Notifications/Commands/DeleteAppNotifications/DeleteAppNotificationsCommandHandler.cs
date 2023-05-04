using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsCommandHandler
    : IRequestHandler<DeleteAppNotificationsCommand, DeleteAppNotificationsResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppNotificationsCommandHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppNotificationsResponse> Handle(
        DeleteAppNotificationsCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppNotificationsRequest request = command
            .DeleteAppNotificationsRequest;

        var response = new DeleteAppNotificationsResponse(
            request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request, _currentUserService.UserId);

        var appNotifications = await _unitOfWork
            .Repository<AppNotification>()
            .ListAsync(specification, cancellationToken);

        if (!appNotifications.Any())
            return response;

        var deletedOn = DateTimeOffset.UtcNow;

        foreach (var appNotification in appNotifications)
            appNotification.UpdateDeletedOn(deletedOn);

        await _unitOfWork
            .Repository<AppNotification>()
            .DeleteRangeAsync(appNotifications, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}