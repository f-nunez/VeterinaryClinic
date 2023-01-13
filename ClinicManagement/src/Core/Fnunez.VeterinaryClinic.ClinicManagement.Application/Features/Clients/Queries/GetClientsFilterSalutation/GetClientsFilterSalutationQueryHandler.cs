using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;

public class GetClientsFilterSalutationQueryHandler
    : IRequestHandler<GetClientsFilterSalutationQuery, GetClientsFilterSalutationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterSalutationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterSalutationResponse> Handle(
        GetClientsFilterSalutationQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterSalutationRequest request = query
            .GetClientsFilterSalutationRequest;

        var response = new GetClientsFilterSalutationResponse(
            request.CorrelationId);

        var specification = new ClientSalutationsSpecification(
            request.SalutationFilterValue);

        var clientSalutations = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clientSalutations is null)
            return response;

        response.ClientSalutations = clientSalutations;

        return response;
    }
}
