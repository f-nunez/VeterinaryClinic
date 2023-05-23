using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;

public class CreateAppointmentTypeCommandHandler
    : IRequestHandler<CreateAppointmentTypeCommand, CreateAppointmentTypeResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIntegrationEventSenderService _integrationEventSenderService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentTypeCommandHandler(
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

    public async Task<CreateAppointmentTypeResponse> Handle(
        CreateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        CreateAppointmentTypeRequest request = command
            .CreateAppointmentTypeRequest;

        var response = new CreateAppointmentTypeResponse(
            request.CorrelationId);

        var newAppointemntType = _mapper.Map<AppointmentType>(request);

        newAppointemntType.SetCreatedBy(_currentUserService.UserId);

        newAppointemntType = await _unitOfWork
            .Repository<AppointmentType>()
            .AddAsync(newAppointemntType, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(newAppointemntType);

        await SendContractsToServiceBusAsync(
            newAppointemntType,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task SendContractsToServiceBusAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendIntegrationEventAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );

        await SendNotificationRequestAsync(
            appointmentType,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendIntegrationEventAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeCreatedIntegrationEventFactory
        (
            appointmentType
        );

        await _integrationEventSenderService.SendAsync(
            factory, correlationId, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        AppointmentType appointmentType,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentTypeCreatedNotificationRequestFactory
        (
            appointmentType,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.SendAsync(
            factory, cancellationToken);
    }
}