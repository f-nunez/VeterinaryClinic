using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterDurationQuery, GetAppointmentTypesFilterDurationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterDurationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterDurationResponse> Handle(
        GetAppointmentTypesFilterDurationQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterDurationRequest request = query
            .GetAppointmentTypesFilterDurationRequest;

        var response = new GetAppointmentTypesFilterDurationResponse(
            request.CorrelationId);

        var specification = new AppointmentTypeDurationsSpecification(
            request.DurationFilterValue);

        var appointmentTypeDurations = await _unitOfWork
            .ReadRepository<AppointmentType>()
            .ListAsync(specification, cancellationToken);

        if (appointmentTypeDurations is null)
            return response;

        response.AppointmentTypeDurations = appointmentTypeDurations;

        return response;
    }
}
