using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClient;

public class GetAppointmentsFilterClientQueryHandler
    : IRequestHandler<GetAppointmentsFilterClientQuery, GetAppointmentsFilterClientResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterClientQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterClientResponse> Handle(
        GetAppointmentsFilterClientQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterClientRequest request = query
            .GetAppointmentsFilterClientRequest;

        var response = new GetAppointmentsFilterClientResponse(
            request.CorrelationId);

        var specification = new ClientFilterValuesSpecification(request);

        var clientFilterValues = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        var count = await _unitOfWork
            .ReadRepository<Client>()
            .CountAsync(specification, cancellationToken);

        if (clientFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<ClientFilterValueDto>(
            clientFilterValues,
            count
        );

        return response;
    }
}