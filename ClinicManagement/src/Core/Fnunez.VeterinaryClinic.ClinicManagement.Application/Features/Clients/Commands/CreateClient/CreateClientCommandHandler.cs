using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandler
    : IRequestHandler<CreateClientCommand, CreateClientResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(
        ICurrentUserService currentUserService,
        IIntegrationEventSenderService integrationEventSenderService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _integrationEventSenderService = integrationEventSenderService;
        _mapper = mapper;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateClientResponse> Handle(
        CreateClientCommand command,
        CancellationToken cancellationToken)
    {
        CreateClientRequest request = command.CreateClientRequest;
        var response = new CreateClientResponse(request.CorrelationId);
        var newClient = _mapper.Map<Client>(request);

        newClient.SetCreatedBy(_currentUserService.UserId);

        newClient = await _unitOfWork
            .Repository<Client>()
            .AddAsync(newClient, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Client = _mapper.Map<ClientDto>(newClient);

        await SendContractsToServiceBusAsync(
            newClient,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendContractsToServiceBusAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            client,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            client,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClientCreatedIntegrationEventFactory(client);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClientCreatedNotificationRequestFactory
        (
            client,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}