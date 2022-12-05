using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClientResponse> Handle(
        UpdateClientCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClientRequest request = command.UpdateClientRequest;
        var response = new UpdateClientResponse(request.CorrelationId);
        var clientToUpdate = _mapper.Map<Client>(request);

        await _unitOfWork.Repository<Client>()
            .UpdateAsync(clientToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Client = _mapper.Map<ClientDto>(clientToUpdate);

        return response;
    }
}