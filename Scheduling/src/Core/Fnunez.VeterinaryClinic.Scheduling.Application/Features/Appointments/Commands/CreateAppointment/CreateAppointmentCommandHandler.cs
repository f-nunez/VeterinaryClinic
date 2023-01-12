using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
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
    private readonly ILogger<CreateAppointmentCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(
        ILogger<CreateAppointmentCommandHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateAppointmentResponse> Handle(
        CreateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        CreateAppointmentRequest request = command.CreateAppointmentRequest;
        var response = new CreateAppointmentResponse(request.CorrelationId);

        var specification = new ScheduledAppointmentSpecification(request);

        var scheduledAppointment = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);
        
        if (scheduledAppointment != null)
            throw new ArgumentException($"An appointment with id: {scheduledAppointment.Id} is already scheduled for patient: {request.PatientId}");

        AppointmentType appointmentType = await GetAppointmentTypeAsync(
            request.AppointmentTypeId, cancellationToken);

        Appointment newAppointment = MapNewAppointment(request);

        AppointmentValidatorService.ValidateDuration(
            newAppointment,
            appointmentType
        );

        await _unitOfWork
            .Repository<Appointment>()
            .AddAsync(newAppointment, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var appointmentDto = _mapper.Map<AppointmentDto>(newAppointment);
        response.Appointment = appointmentDto;
        _logger.LogInformation(appointmentDto.ToString());

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

        return newAppointment;
    }
}