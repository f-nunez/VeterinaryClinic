using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler
    : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(
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

    public async Task<UpdateClientResponse> Handle(
        UpdateClientCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClientRequest request = command.UpdateClientRequest;
        var response = new UpdateClientResponse(request.CorrelationId);

        var clientToUpdate = await _unitOfWork
            .Repository<Client>()
            .GetByIdAsync(request.ClientId, cancellationToken);

        if (clientToUpdate is null)
            throw new NotFoundException(
                nameof(clientToUpdate), request.ClientId);

        await UpdateClientAsync(request, clientToUpdate, cancellationToken);

        response.Client = _mapper.Map<ClientDto>(clientToUpdate);

        await SendContractsToServiceBusAsync(
            clientToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task UpdateClientAsync(
        UpdateClientRequest request,
        Client client,
        CancellationToken cancellationToken)
    {
        var preferredLanguage = (PreferredLanguage)request.PreferredLanguage;

        client.UpdateEmailAddress(request.EmailAddress);
        client.UpdateFullName(request.FullName);
        client.UpdatePreferredDoctorId(request.PreferredDoctorId);
        client.UpdatePreferredLanguage(preferredLanguage);
        client.UpdatePreferredName(request.PreferredName);
        client.UpdateSalutation(request.Salutation);
        client.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Client>()
            .UpdateAsync(client, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
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
        var factory = new ClientUpdatedIntegrationEventFactory(client);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Client client,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClientUpdatedNotificationRequestFactory(
            client,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}