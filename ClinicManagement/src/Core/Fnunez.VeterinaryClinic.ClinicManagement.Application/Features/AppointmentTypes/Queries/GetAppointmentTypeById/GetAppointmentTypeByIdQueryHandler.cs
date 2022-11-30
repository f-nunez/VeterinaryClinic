using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class GetAppointmentTypeByIdQueryHandler : IRequestHandler<GetAppointmentTypeByIdQuery, GetAppointmentTypeByIdResponse>
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
        GetAppointmentTypeByIdRequest request = query.GetAppointmentTypeByIdRequest;
        var response = new GetAppointmentTypeByIdResponse(request.CorrelationId);

        var appointmentType = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (appointmentType is null)
            return response;

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentType);

        return response;
    }
}
