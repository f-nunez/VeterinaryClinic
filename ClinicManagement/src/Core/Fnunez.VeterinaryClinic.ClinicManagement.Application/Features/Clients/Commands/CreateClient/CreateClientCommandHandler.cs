using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateClientResponse> Handle(
        CreateClientCommand command,
        CancellationToken cancellationToken)
    {
        CreateClientRequest request = command.CreateClientRequest;
        var response = new CreateClientResponse(request.CorrelationId);
        var newClient = _mapper.Map<Client>(request);

        newClient = await _unitOfWork.Repository<Client>()
            .AddAsync(newClient, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Client = _mapper.Map<ClientDto>(newClient);

        await SendIntegrationEventAsync(
            newClient,
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
        var message = new ClientCreatedIntegrationEventContract
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
            new ClientCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}
