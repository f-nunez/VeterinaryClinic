using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;

public class GetClientsFilterFullNameQueryHandler
    : IRequestHandler<GetClientsFilterFullNameQuery, GetClientsFilterFullNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterFullNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterFullNameResponse> Handle(
        GetClientsFilterFullNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterFullNameRequest request = query
            .GetClientsFilterFullNameRequest;

        var response = new GetClientsFilterFullNameResponse(
            request.CorrelationId);

        var specification = new ClientFullNamesSpecification(
            request.FullNameFilterValue);

        var clientFullNames = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clientFullNames is null)
            return response;

        response.ClientFullNames = clientFullNames;

        return response;
    }
}