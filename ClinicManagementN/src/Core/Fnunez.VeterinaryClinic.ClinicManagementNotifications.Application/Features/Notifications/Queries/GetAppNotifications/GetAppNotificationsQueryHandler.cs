using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsQueryHandler
    : IRequestHandler<GetAppNotificationsQuery, GetAppNotificationsResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppNotificationsQueryHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppNotificationsResponse> Handle(
        GetAppNotificationsQuery query,
        CancellationToken cancellationToken)
    {
        var request = query.GetNotificationsRequest;
        var response = new GetAppNotificationsResponse(request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request, _currentUserService.UserId);

        var appNotifications = await _unitOfWork
            .ReadRepository<AppNotification>()
            .ListAsync(specification, cancellationToken);

        var appNotificationsCount = await _unitOfWork
            .ReadRepository<AppNotification>()
            .CountAsync(specification, cancellationToken);

        if (appNotifications is null)
            return response;

        var appNotificationDtos = _mapper
            .Map<List<AppNotificationDto>>(appNotifications);

        response.AppNotifications = appNotificationDtos;
        response.Count = appNotificationsCount;

        return response;
    }
}