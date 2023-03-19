using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridQueryHandler
    : IRequestHandler<GetAppNotificationsDataGridQuery, GetAppNotificationsDataGridResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppNotificationsDataGridQueryHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppNotificationsDataGridResponse> Handle(
        GetAppNotificationsDataGridQuery query,
        CancellationToken cancellationToken)
    {
        GetAppNotificationsDataGridRequest request = query
            .GetAppNotificationsDataGridRequest;

        var response = new GetAppNotificationsDataGridResponse(
            request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request, _currentUserService.UserId);

        var appNotifications = await _unitOfWork
            .ReadRepository<AppNotification>()
            .ListAsync(specification, cancellationToken);

        int count = await _unitOfWork
            .ReadRepository<AppNotification>()
            .CountAsync(specification, cancellationToken);

        if (appNotifications is null)
            return response;

        var appNotificationDtos = _mapper
            .Map<List<AppNotificationDto>>(appNotifications);

        response.DataGridResponse = new DataGridResponse<AppNotificationDto>(
            appNotificationDtos,
            count
        );

        return response;
    }
}