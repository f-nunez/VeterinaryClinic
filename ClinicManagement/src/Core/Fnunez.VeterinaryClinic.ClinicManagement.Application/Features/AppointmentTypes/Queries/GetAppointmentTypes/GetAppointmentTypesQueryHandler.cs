using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class GetAppointmentTypesQueryHandler
    : IRequestHandler<GetAppointmentTypesQuery, GetAppointmentTypesResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesResponse> Handle(
        GetAppointmentTypesQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesRequest request = query.GetAppointmentTypesRequest;
        var response = new GetAppointmentTypesResponse(request.CorrelationId);

        var specification = new AppointmentTypesSpecification(
            request);

        var appointmentTypes = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        int count = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .CountAsync(specification, cancellationToken);

        if (appointmentTypes is null)
            return response;

        var appointmentTypeDtos = _mapper
            .Map<List<AppointmentTypeDto>>(appointmentTypes);

        response.DataGridResponse = new DataGridResponse<AppointmentTypeDto>(
            appointmentTypeDtos,
            count
        );

        return response;
    }
}