using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.Services;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandHandler
    : IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmailRequestService _emailRequestService;
    private readonly ILogger<UpdateAppointmentCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentCommandHandler(
        ICurrentUserService currentUserService,
        IEmailRequestService emailRequestService,
        ILogger<UpdateAppointmentCommandHandler> logger,
        IMapper mapper,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _emailRequestService = emailRequestService;
        _logger = logger;
        _mapper = mapper;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAppointmentResponse> Handle(
        UpdateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        UpdateAppointmentRequest request = command.UpdateAppointmentRequest;
        var response = new UpdateAppointmentResponse(request.CorrelationId);

        var updateResult = await UpdateAppointmentAsync(
            request, cancellationToken);

        response.Appointment = _mapper
            .Map<AppointmentDto>(updateResult.Appointment);

        await SendContractsToServiceBusAsync(
            updateResult.Appointment,
            updateResult.IsChangedStartOn,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task<(Appointment Appointment, bool IsChangedStartOn)> UpdateAppointmentAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointmentType = await GetAppointmentTypeAsync(
            request, cancellationToken);

        var appointmentToUpdate = await GetAppointmentAsync(
            request, cancellationToken);

        await ValidateScheduledAppointmentAsync(
            appointmentToUpdate, request, cancellationToken);

        bool isChangedStartOn = appointmentToUpdate
            .DateRange.StartOn != request.StartOn;

        MapAppointmentToUpdate(appointmentToUpdate, request, isChangedStartOn);

        AppointmentValidatorService.ValidateDuration(
            appointmentToUpdate, appointmentType);

        await _unitOfWork
            .Repository<Appointment>()
            .UpdateAsync(appointmentToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return (appointmentToUpdate, isChangedStartOn);
    }

    private async Task<Appointment> GetAppointmentAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new AppointmentSpecification(
            request.AppointmentId);

        var appointment = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment),
                request.AppointmentId
            );

        return appointment;
    }

    private async Task<AppointmentType> GetAppointmentTypeAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointmentType = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(request.AppointmentTypeId, cancellationToken);

        if (appointmentType is null)
            throw new NotFoundException(
                nameof(appointmentType),
                request.AppointmentTypeId
            );

        return appointmentType;
    }

    private void MapAppointmentToUpdate(
        Appointment appointment,
        UpdateAppointmentRequest request,
        bool isChangedStartOn)
    {
        var dateRange = new DateTimeOffsetRange(
            request.StartOn, request.EndOn);

        appointment.SetUpdatedBy(_currentUserService.UserId);
        appointment.UpdateAppointmentType(request.AppointmentTypeId);
        appointment.UpdateDateRange(dateRange);
        appointment.UpdateDescription(request.Description);
        appointment.UpdateDoctor(request.DoctorId);
        appointment.UpdateRoom(request.RoomId);
        appointment.UpdateTitle(request.Title);

        if (isChangedStartOn)
            appointment.ResetConfirmOn();
    }

    private async Task SendContractsToServiceBusAsync(
        Appointment appointment,
        bool isChangedStartOn,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendNotificationRequestAsync(
            appointment,
            correlationId,
            cancellationToken
        );

        if (isChangedStartOn)
            await SendEmailRequestAsync(
                appointment,
                correlationId,
                cancellationToken
            );
    }

    private async Task SendEmailRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentUpdatedEmailRequestFactory(
            appointment,
            correlationId,
            _currentUserService.UserId
        );

        await _emailRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentUpdatedNotificationRequestFactory(
            appointment,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }

    private async Task ValidateScheduledAppointmentAsync(
        Appointment appointmentToUpdate,
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new ScheduledAppointmentSpecification(
            request,
            appointmentToUpdate.ClientId,
            appointmentToUpdate.PatientId
        );

        var scheduledAppointment = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (scheduledAppointment != null)
            throw new ArgumentException($"An appointment with id: {scheduledAppointment.Id} is already scheduled for patient: {scheduledAppointment.PatientId}");
    }
}