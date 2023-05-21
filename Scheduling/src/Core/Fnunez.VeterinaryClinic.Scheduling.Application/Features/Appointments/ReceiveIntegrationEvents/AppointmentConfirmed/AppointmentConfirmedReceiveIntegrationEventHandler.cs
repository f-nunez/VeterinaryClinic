using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.ReceiveIntegrationEvents.AppointmentConfirmed;

public class AAppointmentConfirmedReceiveIntegrationEventHandler
    : INotificationHandler<AppointmentConfirmedReceiveIntegrationEvent>
{
    private const string SchedulingAppId = "00000001-0000-0000-0000-000000000000";
    private readonly IEmailRequestService _emailRequestService;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfwork;

    public AAppointmentConfirmedReceiveIntegrationEventHandler(
        IEmailRequestService emailRequestService,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfwork)
    {
        _emailRequestService = emailRequestService;
        _notificationRequestService = notificationRequestService;
        _unitOfwork = unitOfwork;
    }

    public async Task Handle(
        AppointmentConfirmedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .AppointmentConfirmedIntegrationEventPublicContract;

        var specification = new AppointmentSpecification(
            contract.AppointmentId);

        var appointment = await _unitOfwork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment), contract.AppointmentId);

        if (appointment.ConfirmOn.HasValue)
            return;

        appointment.UpdateConfirmOn(DateTimeOffset.UtcNow);

        await _unitOfwork
            .Repository<Appointment>()
            .UpdateAsync(appointment, cancellationToken);

        await _unitOfwork.CommitAsync(cancellationToken);

        await SendContractsToServiceBusAsync(
            appointment, contract.CorrelationId, cancellationToken);
    }

    private async Task SendContractsToServiceBusAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        await SendNotificationRequestAsync(
            appointment,
            correlationId,
            cancellationToken
        );

        await SendEmailRequestAsync(
            appointment,
            correlationId,
            cancellationToken
        );
    }

    private async Task SendEmailRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentConfirmedEmailRequestFactory(
            appointment,
            correlationId,
            SchedulingAppId
        );

        await _emailRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentConfirmedNotificationRequestFactory(
            appointment,
            correlationId,
            SchedulingAppId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}