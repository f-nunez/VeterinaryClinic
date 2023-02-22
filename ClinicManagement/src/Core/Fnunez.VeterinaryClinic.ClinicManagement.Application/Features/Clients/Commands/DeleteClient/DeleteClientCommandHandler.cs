using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientDeleted;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler
    : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
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

        var clientToDelete = await _unitOfWork
            .Repository<Client>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (clientToDelete is null)
            throw new NotFoundException(nameof(clientToDelete), request.Id);
        
        clientToDelete.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Client>()
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
