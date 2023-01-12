using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryHandler
    : IRequestHandler<GetAppointmentDetailQuery, GetAppointmentDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentDetailQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentDetailResponse> Handle(
        GetAppointmentDetailQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentDetailRequest request = query
            .GetAppointmentDetailRequest;

        var response = new GetAppointmentDetailResponse(
            request.AppointmentId);

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

        response.Appointment = _mapper.Map<AppointmentDetailDto>(appointment);

        return response;
    }
}