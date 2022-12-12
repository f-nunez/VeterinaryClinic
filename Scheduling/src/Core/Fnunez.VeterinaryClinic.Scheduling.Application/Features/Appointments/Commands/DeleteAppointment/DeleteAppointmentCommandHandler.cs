using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, DeleteAppointmentResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentResponse> Handle(
        DeleteAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentRequest request = command.DeleteAppointmentRequest;
        var response = new DeleteAppointmentResponse(request.CorrelationId);

        var specification = new ScheduleByIdIncludeAppointmentsSpecification(
            request.ScheduleId);

        var schedule = await _unitOfWork.Repository<Schedule>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (schedule is null)
            throw new NotFoundException(nameof(schedule), request.ScheduleId);

        var appointmentToDelete = schedule.Appointments
            .FirstOrDefault(a => a.Id == request.AppointmentId);

        if (appointmentToDelete is null)
            throw new NotFoundException(
                nameof(appointmentToDelete),
                request.AppointmentId
            );

        schedule.RemoveAppointment(appointmentToDelete);

        await _unitOfWork.Repository<Schedule>().UpdateAsync(schedule);

        await _unitOfWork.CommitAsync();

        response.Schedule = _mapper.Map<ScheduleDto>(schedule);

        return response;
    }
}
