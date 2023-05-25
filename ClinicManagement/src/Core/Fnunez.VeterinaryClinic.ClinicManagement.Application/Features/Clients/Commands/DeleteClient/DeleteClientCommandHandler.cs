using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler
    : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(
        ICurrentUserService currentUserService,
        IIntegrationEventSenderService integrationEventSenderService,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _integrationEventSenderService = integrationEventSenderService;
        _mediator = mediator;
        _notificationRequestService = notificationRequestService;
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

        await SendContractsToServiceBusAsync(
            clientToDelete,
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
        var factory = new ClientDeletedIntegrationEventFactory(client);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClientDeletedNotificationRequestFactory(
            client,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}