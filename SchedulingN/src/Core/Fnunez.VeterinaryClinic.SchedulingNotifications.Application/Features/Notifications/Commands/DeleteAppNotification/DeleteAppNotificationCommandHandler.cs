using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationCommandHandler : IRequestHandler<DeleteAppNotificationCommand, DeleteAppNotificationResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppNotificationCommandHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppNotificationResponse> Handle(
        DeleteAppNotificationCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.DeleteAppNotificationRequest;

        var response = new DeleteAppNotificationResponse(
            request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request.AppNotificationId, _currentUserService.UserId);

        var appNotification = await _unitOfWork
            .Repository<AppNotification>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appNotification is null)
            throw new NotFoundException(
                nameof(appNotification), request.AppNotificationId);

        if (appNotification.DeletedOn.HasValue)
            return response;

        appNotification.UpdateDeletedOn(DateTimeOffset.UtcNow);

        await _unitOfWork
            .Repository<AppNotification>()
            .DeleteAsync(appNotification, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}