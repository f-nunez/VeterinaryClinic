using AutoMapper;
using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.SendIntegrationEvents.ClientUpdated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler
    : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(
        ICurrentUserService currentUserService,
        IMapper mapper,
        IMediator mediator,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
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

        clientToUpdate.UpdateEmailAddress(request.EmailAddress);
        clientToUpdate.UpdateFullName(request.FullName);
        clientToUpdate.UpdatePreferredDoctorId(request.PreferredDoctorId);
        clientToUpdate.UpdatePreferredName(request.PreferredName);
        clientToUpdate.UpdateSalutation(request.Salutation);
        clientToUpdate.SetUpdatedBy(_currentUserService.UserId);

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

        await SendNotificationRequestAsync(
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

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}