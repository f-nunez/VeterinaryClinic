using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.SendIntegrationEvents.AppointmentTypeUpdated;
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
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentTypeCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
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

        appointmentTypeToUpdate.UpdateCode(request.Code);
        appointmentTypeToUpdate.UpdateDuration(request.Duration);
        appointmentTypeToUpdate.UpdateName(request.Name);
        appointmentTypeToUpdate.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<AppointmentType>()
            .UpdateAsync(appointmentTypeToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentTypeToUpdate);

        await SendIntegrationEventAsync(
            appointmentTypeToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            appointmentTypeToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new AppointmentTypeUpdatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            AppointmentTypeCode = appointmentType.Code,
            AppointmentTypeDuration = appointmentType.Duration,
            AppointmentTypeId = appointmentType.Id,
            AppointmentTypeName = appointmentType.Name
        };

        await _mediator.Publish(
            new AppointmentTypeUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }

    private async Task SendNotificationRequestAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeUpdatedNotificationRequestFactory(
            appointmentType,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}