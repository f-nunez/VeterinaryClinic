using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler
    : IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdResponse>
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

        var specification = new AppointmentByIdSpecification(
            request.AppointmentId);

        var appointment = await _unitOfWork
            .ReadRepository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment),
                request.AppointmentId
            );

        response.Appointment = _mapper.Map<AppointmentDto>(appointment);

        return response;
    }
}