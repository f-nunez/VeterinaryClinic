using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadCommandHandler
    : IRequestHandler<MarkAppNotificationAsReadCommand, MarkAppNotificationAsReadResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public MarkAppNotificationAsReadCommandHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<MarkAppNotificationAsReadResponse> Handle(
        MarkAppNotificationAsReadCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.MarkNotificationAsReadRequest;

        var response = new MarkAppNotificationAsReadResponse(
            request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request.AppNotificationId, _currentUserService.UserId);

        var appNotification = await _unitOfWork
            .Repository<AppNotification>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appNotification is null)
            throw new NotFoundException(
                nameof(appNotification), request.AppNotificationId);

        if (appNotification.ReadOn.HasValue)
            return response;

        appNotification.UpdateReadOn(DateTimeOffset.UtcNow);

        await _unitOfWork
            .Repository<AppNotification>()
            .UpdateAsync(appNotification, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}