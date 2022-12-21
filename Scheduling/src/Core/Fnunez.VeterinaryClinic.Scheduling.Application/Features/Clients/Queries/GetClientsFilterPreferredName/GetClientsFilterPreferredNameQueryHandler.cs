using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameQueryHandler
    : IRequestHandler<GetClientsFilterPreferredNameQuery, GetClientsFilterPreferredNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterPreferredNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterPreferredNameResponse> Handle(
        GetClientsFilterPreferredNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterPreferredNameRequest request = query
            .GetClientsFilterPreferredNameRequest;

        var response = new GetClientsFilterPreferredNameResponse(
            request.CorrelationId);

        var specification = new ClientPreferredNamesSpecification(
            request.PreferredNameFilterValue);

        var clientPreferredNames = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clientPreferredNames is null)
            return response;

        response.ClientPreferredNames = clientPreferredNames;

        return response;
    }
}