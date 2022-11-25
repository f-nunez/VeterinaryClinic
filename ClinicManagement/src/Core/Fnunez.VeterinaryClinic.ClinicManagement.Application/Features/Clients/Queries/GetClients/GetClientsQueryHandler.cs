using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, GetClientsResponse>
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
        var specification = new GetClientsOrderedByFullNameSpecification();

        var clients = await _unitOfWork.Repository<Client>()
            .ListAsync(specification, cancellationToken);

        if (clients is null)
            return response;

        response.Clients = _mapper.Map<List<ClientDto>>(clients);
        response.Count = response.Clients.Count;

        return response;
    }
}
