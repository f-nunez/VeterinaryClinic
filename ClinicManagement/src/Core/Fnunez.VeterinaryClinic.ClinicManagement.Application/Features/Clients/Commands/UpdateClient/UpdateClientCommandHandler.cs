using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler
    : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClientResponse> Handle(
        UpdateClientCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClientRequest request = command.UpdateClientRequest;
        var response = new UpdateClientResponse(request.CorrelationId);
        var clientToUpdate = _mapper.Map<Client>(request);

        await _unitOfWork
            .Repository<Client>()
            .UpdateAsync(clientToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Client = _mapper.Map<ClientDto>(clientToUpdate);

        await SendIntegrationEventAsync(
            clientToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendIntegrationEventAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new ClientUpdatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            ClientEmailAddress = client.EmailAddress,
            ClientFullName = client.FullName,
            ClientId = client.Id,
            ClientPreferredDoctorId = client.PreferredDoctorId,
            ClientPreferredName = client.PreferredName,
            ClientSalutation = client.Salutation
        };

        await _mediator.Publish(
            new ClientUpdatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}