using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterNameQuery, GetAppointmentTypesFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterNameQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterNameResponse> Handle(
        GetAppointmentTypesFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterNameRequest request = query
            .GetAppointmentTypesFilterNameRequest;

        var response = new GetAppointmentTypesFilterNameResponse(
            request.CorrelationId);

        var specification = new AppointmentTypeNamesSpecification(
            request.NameFilterValue);

        var appointmentTypeNames = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        if (appointmentTypeNames is null)
            return response;

        response.AppointemntTypeNames = appointmentTypeNames;

        return response;
    }
}