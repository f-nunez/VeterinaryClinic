using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

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