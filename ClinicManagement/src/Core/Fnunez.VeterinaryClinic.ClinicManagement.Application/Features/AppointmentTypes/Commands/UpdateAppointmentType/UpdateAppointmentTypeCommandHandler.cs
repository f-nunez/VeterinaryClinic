using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandHandler : IRequestHandler<UpdateAppointmentTypeCommand, UpdateAppointmentTypeResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentTypeCommandHandler(
        ICurrentUserService currentUserService,
        IIntegrationEventSenderService integrationEventSenderService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _integrationEventSenderService = integrationEventSenderService;
        _mapper = mapper;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAppointmentTypeResponse> Handle(
        UpdateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        UpdateAppointmentTypeRequest request = command
            .UpdateAppointmentTypeRequest;

        var response = new UpdateAppointmentTypeResponse(
            request.CorrelationId);

        var appointmentTypeToUpdate = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (appointmentTypeToUpdate is null)
            throw new NotFoundException(
                nameof(appointmentTypeToUpdate), request.Id);

        await UpdateAppointmentTypeAsync(
            request, appointmentTypeToUpdate, cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentTypeToUpdate);

        await SendContractsToServiceBusAsync(
            appointmentTypeToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task UpdateAppointmentTypeAsync(
        UpdateAppointmentTypeRequest request,
        AppointmentType appointmentType,
        CancellationToken cancellationToken)
    {
        appointmentType.UpdateCode(request.Code);
        appointmentType.UpdateDuration(request.Duration);
        appointmentType.UpdateName(request.Name);
        appointmentType.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<AppointmentType>()
            .UpdateAsync(appointmentType, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task SendContractsToServiceBusAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeUpdatedIntegrationEventFactory
        (
            appointmentType
        );

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeUpdatedNotificationRequestFactory
        (
            appointmentType,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}