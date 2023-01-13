using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressQueryHandler
    : IRequestHandler<GetClientsFilterEmailAddressQuery, GetClientsFilterEmailAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterEmailAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterEmailAddressResponse> Handle(
        GetClientsFilterEmailAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterEmailAddressRequest request = query
            .GetClientsFilterEmailAddressRequest;

        var response = new GetClientsFilterEmailAddressResponse(
            request.CorrelationId);

        var specification = new ClientEmailAddressesSpecification(
            request.EmailAddressFilterValue);

        var clientEmailAddresses = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clientEmailAddresses is null)
            return response;

        response.ClientEmailAddresses = clientEmailAddresses;

        return response;
    }
}