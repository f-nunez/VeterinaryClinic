using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
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

        var specification = new AppointmentsSpecification(request);

        var appointments = await _unitOfWork
            .ReadRepository<Appointment>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Appointment>()
            .CountAsync(specification, cancellationToken);

        if (appointments is null)
            return response;

        response.DataGridResponse = new DataGridResponse<AppointmentDto>(
            _mapper.Map<List<AppointmentDto>>(appointments),
            count
        );

        return response;
    }
}