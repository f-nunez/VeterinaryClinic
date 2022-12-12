using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
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

        var schedule = await GetScheduleAsync(
            request.ScheduleId, cancellationToken);

        var appointmentType = await GetAppointmentTypeAsync(
            request.AppointmentTypeId, cancellationToken);

        var newAppointment = MapNewAppointment(
            request, appointmentType.Duration);

        schedule.AddAppointment(newAppointment);

        await _unitOfWork.Repository<Schedule>()
            .UpdateAsync(schedule, cancellationToken);

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

    private async Task<Schedule> GetScheduleAsync(
        Guid scheduleId,
        CancellationToken cancellationToken)
    {
        var specification = new ScheduleByIdIncludeAppointmentsSpecification(
            scheduleId);

        var schedule = await _unitOfWork.Repository<Schedule>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (schedule is null)
            throw new NotFoundException(
                nameof(schedule),
                scheduleId
            );

        return schedule;
    }

    private Appointment MapNewAppointment(
        CreateAppointmentRequest request,
        int AppointmentDuration)
    {
        var dateRange = new DateTimeOffsetRange(
           request.DateOfAppointment,
           TimeSpan.FromMinutes(AppointmentDuration)
       );

        var newAppointment = new Appointment(
            Guid.NewGuid(),
            request.AppointmentTypeId,
            request.ClientId,
            request.DoctorId,
            request.PatientId,
            request.RoomId,
            request.ScheduleId,
            dateRange,
            request.Title
        );

        return newAppointment;
    }
}
