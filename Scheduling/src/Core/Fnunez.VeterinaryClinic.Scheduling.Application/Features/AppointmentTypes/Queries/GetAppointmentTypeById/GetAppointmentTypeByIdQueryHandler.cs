using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public class GetAppointmentTypeByIdQueryHandler
    : IRequestHandler<GetAppointmentTypeByIdQuery, GetAppointmentTypeByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypeByIdQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypeByIdResponse> Handle(
        GetAppointmentTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypeByIdRequest request = query
            .GetAppointmentTypeByIdRequest;

        var response = new GetAppointmentTypeByIdResponse(
            request.CorrelationId);

        var appointmentType = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (appointmentType is null)
            throw new NotFoundException(nameof(appointmentType), request.Id);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentType);

        return response;
    }
}
