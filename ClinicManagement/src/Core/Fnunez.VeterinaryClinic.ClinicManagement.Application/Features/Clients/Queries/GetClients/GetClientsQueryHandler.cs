using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryHandler
    : IRequestHandler<GetClientsQuery, GetClientsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsResponse> Handle(
        GetClientsQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsRequest request = query.GetClientsRequest;
        var response = new GetClientsResponse(request.CorrelationId);
        var specification = new ClientsSpecification(request);

        var clients = await _unitOfWork
            .ReadRepository<Client>()
            .ListAsync(specification, cancellationToken);

        int count = await _unitOfWork
            .ReadRepository<Client>()
            .CountAsync(specification, cancellationToken);

        if (clients is null)
            return response;

        var clientsDtos = _mapper.Map<List<ClientDto>>(clients);

        response.DataGridResponse = new DataGridResponse<ClientDto>(
            clientsDtos,
            count
        );

        return response;
    }
}