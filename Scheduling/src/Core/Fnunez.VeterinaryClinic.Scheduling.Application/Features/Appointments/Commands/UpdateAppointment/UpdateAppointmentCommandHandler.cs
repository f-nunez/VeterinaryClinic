using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
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
    private readonly ILogger<UpdateAppointmentCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentCommandHandler(
        ILogger<UpdateAppointmentCommandHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAppointmentResponse> Handle(
        UpdateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        UpdateAppointmentRequest request = command.UpdateAppointmentRequest;
        var response = new UpdateAppointmentResponse(request.CorrelationId);

        var appointmentType = await GetAppointmentTypeAsync(
            request, cancellationToken);

        var appointmentToUpdate = await GetAppointmentAsync(
            request, cancellationToken);

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

        MapAppointmentToUpdate(appointmentToUpdate, request);

        AppointmentValidatorService.ValidateDuration(
            appointmentToUpdate, appointmentType);

        await _unitOfWork
            .Repository<Appointment>()
            .UpdateAsync(appointmentToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var appointmentDto = _mapper.Map<AppointmentDto>(appointmentToUpdate);
        response.Appointment = appointmentDto;
        _logger.LogInformation(appointmentDto.ToString());

        return response;
    }

    private async Task<Appointment> GetAppointmentAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork
            .Repository<Appointment>()
            .GetByIdAsync(request.AppointmentId, cancellationToken);

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
        UpdateAppointmentRequest request)
    {
        var dateRange = new DateTimeOffsetRange(request.StartOn, request.EndOn);

        appointment.UpdateAppointmentType(request.AppointmentTypeId);
        appointment.UpdateDateRange(dateRange);
        appointment.UpdateDescription(request.Description);
        appointment.UpdateDoctor(request.DoctorId);
        appointment.UpdateRoom(request.RoomId);
        appointment.UpdateTitle(request.Title);
    }
}