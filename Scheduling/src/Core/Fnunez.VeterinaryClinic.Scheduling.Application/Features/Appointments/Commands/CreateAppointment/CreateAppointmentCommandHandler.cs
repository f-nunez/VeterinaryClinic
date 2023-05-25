using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.Services;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmailRequestService _emailRequestService;
    private readonly ILogger<CreateAppointmentCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(
        ICurrentUserService currentUserService,
        IEmailRequestService emailRequestService,
        ILogger<CreateAppointmentCommandHandler> logger,
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

    public async Task<CreateAppointmentResponse> Handle(
        CreateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        CreateAppointmentRequest request = command.CreateAppointmentRequest;
        var response = new CreateAppointmentResponse(request.CorrelationId);

        await ValidateScheduledAppointmentAsync(request, cancellationToken);

        AppointmentType appointmentType = await GetAppointmentTypeAsync(
            request.AppointmentTypeId, cancellationToken);

        Appointment newAppointment = MapNewAppointment(request);

        AppointmentValidatorService.ValidateDuration(
            newAppointment, appointmentType);

        await SaveAppointmentAsync(newAppointment, cancellationToken);

        response.Appointment = _mapper.Map<AppointmentDto>(newAppointment);

        Appointment currentAppointment = await GetCurrentAppointmentAsync(
            newAppointment.Id, cancellationToken);

        await SendContractsToServiceBusAsync(
            currentAppointment, request.CorrelationId, cancellationToken);

        return response;
    }

    private async Task<AppointmentType> GetAppointmentTypeAsync(
        int appointmentTypeId,
        CancellationToken cancellationToken)
    {
        var appointmentType = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(appointmentTypeId, cancellationToken);

        if (appointmentType is null)
            throw new NotFoundException(
                nameof(appointmentType),
                appointmentTypeId
            );

        return appointmentType;
    }

    private async Task<Appointment> GetCurrentAppointmentAsync(
        Guid appointmentId,
        CancellationToken cancellationToken)
    {
        var specification = new AppointmentSpecification(
            appointmentId);

        var appointment = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment),
                appointmentId
            );

        return appointment;
    }

    private Appointment MapNewAppointment(CreateAppointmentRequest request)
    {
        var dateRange = new DateTimeOffsetRange(request.StartOn, request.EndOn);

        var newAppointment = new Appointment(
            Guid.NewGuid(),
            request.AppointmentTypeId,
            request.ClientId,
            request.ClinicId,
            request.DoctorId,
            request.PatientId,
            request.RoomId,
            dateRange,
            request.Description,
            request.Title
        );

        newAppointment.SetCreatedBy(_currentUserService.UserId);

        return newAppointment;
    }

    private async Task SaveAppointmentAsync(
        Appointment appointment,
        CancellationToken cancellationToken)
    {
        await _unitOfWork
            .Repository<Appointment>()
            .AddAsync(appointment, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task SendContractsToServiceBusAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendNotificationRequestAsync(
            appointment,
            correlationId,
            cancellationToken
        );

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
        var factory = new AppointmentCreatedEmailRequestFactory(
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
        var factory = new AppointmentCreatedNotificationRequestFactory(
            appointment,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }

    private async Task ValidateScheduledAppointmentAsync(
        CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new ScheduledAppointmentSpecification(request);

        var scheduledAppointment = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (scheduledAppointment != null)
            throw new ArgumentException($"An appointment with id: {scheduledAppointment.Id} is already scheduled for patient: {request.PatientId}");
    }
}