using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterIdQuery, GetAppointmentTypesFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterIdResponse> Handle(
        GetAppointmentTypesFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterIdRequest request = query
            .GetAppointmentTypesFilterIdRequest;

        var response = new GetAppointmentTypesFilterIdResponse(
            request.CorrelationId);

        var specification = new AppointmentTypeIdsSpecification(
            request.IdFilterValue);

        var appointmentTypeIds = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        if (appointmentTypeIds is null)
            return response;

        response.AppointmentTypeIds = appointmentTypeIds;

        return response;
    }
}
