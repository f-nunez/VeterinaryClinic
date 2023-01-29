using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
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

        await SendIntegrationEventAsync(
            request.Id,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        int clientId,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new ClientDeletedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            ClientId = clientId
        };

        await _mediator.Publish(
            new ClientDeletedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
