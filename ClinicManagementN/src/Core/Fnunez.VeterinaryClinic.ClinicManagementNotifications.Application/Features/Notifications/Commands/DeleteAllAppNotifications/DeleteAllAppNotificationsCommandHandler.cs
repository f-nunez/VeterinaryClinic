using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;

public class DeleteAllAppNotificationsCommandHandler
    : IRequestHandler<DeleteAllAppNotificationsCommand, DeleteAllAppNotificationsResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllAppNotificationsCommandHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAllAppNotificationsResponse> Handle(
        DeleteAllAppNotificationsCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.DeleteAllAppNotificationsRequest;

        var response = new DeleteAllAppNotificationsResponse(
            request.CorrelationId);

        var specification = new AppNotificationSpecification(
            _currentUserService.UserId);

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