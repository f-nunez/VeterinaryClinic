using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteClientResponse> Handle(
        DeleteClientCommand command,
        CancellationToken cancellationToken)
    {
        DeleteClientRequest request = command.DeleteClientRequest;
        var response = new DeleteClientResponse(request.CorrelationId);
        var clientToDelete = _mapper.Map<Client>(request);

        await _unitOfWork.Repository<Client>()
            .DeleteAsync(clientToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
