using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;

public class GetClientsFilterIdQueryHandler
    : IRequestHandler<GetClientsFilterIdQuery, GetClientsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterIdResponse> Handle(
        GetClientsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterIdRequest request = query.GetClientsFilterIdRequest;
        var response = new GetClientsFilterIdResponse(request.CorrelationId);
        var specification = new ClientIdsSpecification(request.IdFilterValue);

        var clientIds = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clientIds is null)
            return response;

        response.ClientIds = clientIds;

        return response;
    }
}