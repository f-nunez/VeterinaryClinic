using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryHandler
    : IRequestHandler<GetAppointmentsQuery, GetAppointmentsResponse>
{
    private readonly ILogger<GetAppointmentsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsQueryHandler(
        ILogger<GetAppointmentsQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsResponse> Handle(
        GetAppointmentsQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsRequest request = query.GetAppointmentsRequest;
        var response = new GetAppointmentsResponse(request.CorrelationId);

        var specification = new ScheduleByIdIncludeAppointmentsThenIncludeClientAndPatientSpecification(
            request.ScheduleId);

        var schedule = await _unitOfWork
            .ReadRepository<Schedule>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (schedule is null)
            throw new NotFoundException(nameof(schedule), request.ScheduleId);

        int conflictedAppointmentsCount = schedule.Appointments
            .Count(a => a.IsPotentiallyConflicting);

        if (conflictedAppointmentsCount > 0)
            _logger.LogInformation(
                $"There are {conflictedAppointmentsCount} conflicted appointments.");

        response.Appointments = _mapper
            .Map<List<AppointmentDto>>(schedule.Appointments);

        response.Count = response.Appointments.Count;

        return response;
    }
}
