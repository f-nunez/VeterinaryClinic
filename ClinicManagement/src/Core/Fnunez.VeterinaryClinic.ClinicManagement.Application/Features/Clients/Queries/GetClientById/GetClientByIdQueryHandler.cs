using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, GetClientByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientByIdResponse> Handle(
        GetClientByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClientByIdRequest request = query.GetClientByIdRequest;
        var response = new GetClientByIdResponse(request.CorrelationId);

        var client = await _unitOfWork.ReadRepository<Client>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.Id);

        response.Client = _mapper.Map<ClientDto>(client);

        return response;
    }
}
