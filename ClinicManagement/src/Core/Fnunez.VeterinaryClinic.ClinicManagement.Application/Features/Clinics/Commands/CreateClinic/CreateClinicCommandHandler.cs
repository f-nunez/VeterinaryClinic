using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandHandler
    : IRequestHandler<CreateClinicCommand, CreateClinicResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClinicCommandHandler(
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

    public async Task<CreateClinicResponse> Handle(
        CreateClinicCommand command,
        CancellationToken cancellationToken)
    {
        CreateClinicRequest request = command.CreateClinicRequest;
        var response = new CreateClinicResponse(request.CorrelationId);
        var newClinic = _mapper.Map<Clinic>(request);

        newClinic.SetCreatedBy(_currentUserService.UserId);

        newClinic = await _unitOfWork
            .Repository<Clinic>()
            .AddAsync(newClinic, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Clinic = _mapper.Map<ClinicDto>(newClinic);

        await SendContractsToServiceBusAsync(
            newClinic,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendContractsToServiceBusAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            clinic,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            clinic,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClinicCreatedIntegrationEventFactory(clinic);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClinicCreatedNotificationRequestFactory
        (
            clinic,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}