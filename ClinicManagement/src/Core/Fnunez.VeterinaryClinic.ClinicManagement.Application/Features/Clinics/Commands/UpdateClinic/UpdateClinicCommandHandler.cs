using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandHandler
    : IRequestHandler<UpdateClinicCommand, UpdateClinicResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler(
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

    public async Task<UpdateClinicResponse> Handle(
        UpdateClinicCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClinicRequest request = command.UpdateClinicRequest;
        var response = new UpdateClinicResponse(request.CorrelationId);

        var clinicToUpdate = await _unitOfWork
            .Repository<Clinic>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (clinicToUpdate is null)
            throw new NotFoundException(nameof(clinicToUpdate), request.Id);

        await UpdateClinicAsync(request, clinicToUpdate, cancellationToken);

        response.Clinic = _mapper.Map<ClinicDto>(clinicToUpdate);

        await SendContractsToServiceBusAsync(
            clinicToUpdate,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task UpdateClinicAsync(
        UpdateClinicRequest request,
        Clinic clinic,
        CancellationToken cancellationToken)
    {
        clinic.UpdateAddress(request.Address);
        clinic.UpdateEmailAddress(request.EmailAddress);
        clinic.UpdateName(request.Name);
        clinic.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Clinic>()
            .UpdateAsync(clinic, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
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
        var factory = new ClinicUpdatedIntegrationEventFactory(clinic);

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Clinic clinic,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new ClinicUpdatedNotificationRequestFactory
        (
            clinic,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}