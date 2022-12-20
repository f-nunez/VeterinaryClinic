using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;
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

        var schedule = await GetScheduleAsync(request, cancellationToken);

        var appointmentType = await GetAppointmentTypeAsync(
            request, cancellationToken);

        var appointmentToUpdate = GetAppointmentToUpdate(schedule, request);

        appointmentToUpdate.UpdateAppointmentType(appointmentType);
        appointmentToUpdate.UpdateDoctor(request.DoctorId);
        appointmentToUpdate.UpdateRoom(request.RoomId);
        appointmentToUpdate.UpdateStartOn(request.StartOn);
        appointmentToUpdate.UpdateTitle(request.Title);

        await _unitOfWork
            .Repository<Schedule>()
            .UpdateAsync(schedule, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var appointmentDto = _mapper.Map<AppointmentDto>(appointmentToUpdate);
        response.Appointment = appointmentDto;
        _logger.LogInformation(appointmentDto.ToString());

        return response;
    }

    private Appointment GetAppointmentToUpdate(
        Schedule schedule,
        UpdateAppointmentRequest request)
    {
        var appointmentToUpdate = schedule.Appointments
            .FirstOrDefault(a => a.Id == request.AppointmentId);

        if (appointmentToUpdate is null)
            throw new NotFoundException(
                nameof(appointmentToUpdate),
                request.AppointmentId
            );

        return appointmentToUpdate;
    }

    private async Task<AppointmentType> GetAppointmentTypeAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointmentType = await _unitOfWork
            .Repository<AppointmentType>()
            .GetByIdAsync(request.AppointmentTypeId);

        if (appointmentType is null)
            throw new NotFoundException(
                nameof(appointmentType),
                request.AppointmentTypeId
            );

        return appointmentType;
    }

    private async Task<Schedule> GetScheduleAsync(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new ScheduleByIdIncludeAppointmentsSpecification(
            request.ScheduleId);

        var schedule = await _unitOfWork
            .Repository<Schedule>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (schedule is null)
            throw new NotFoundException(
                nameof(schedule),
                request.ScheduleId
            );

        return schedule;
    }
}