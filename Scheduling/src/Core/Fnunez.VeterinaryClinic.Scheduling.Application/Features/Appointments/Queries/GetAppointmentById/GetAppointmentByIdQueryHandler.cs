using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentByIdQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentByIdResponse> Handle(
        GetAppointmentByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentByIdRequest request = query.GetAppointmentByIdRequest;
        var response = new GetAppointmentByIdResponse(request.CorrelationId);

        var specification = new ScheduleByIdIncludeAppointmentsThenIncludeClientAndPatientSpecification(
            request.ScheduleId);

        var schedule = await _unitOfWork.ReadRepository<Schedule>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (schedule is null)
            throw new NotFoundException(nameof(schedule), request.ScheduleId);

        var appointment = schedule.Appointments
            .FirstOrDefault(x => x.Id == request.AppointmentId);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment),
                request.AppointmentId
            );

        response.Appointment = _mapper.Map<AppointmentDto>(appointment);

        return response;
    }
}
