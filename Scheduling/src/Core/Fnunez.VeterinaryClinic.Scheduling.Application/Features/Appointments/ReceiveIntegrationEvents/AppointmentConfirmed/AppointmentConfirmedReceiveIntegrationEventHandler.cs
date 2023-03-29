using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.ReceiveIntegrationEvents.AppointmentConfirmed;

public class AAppointmentConfirmedReceiveIntegrationEventHandler
    : INotificationHandler<AppointmentConfirmedReceiveIntegrationEvent>
{
    private readonly IEmailRequestService _emailRequestService;
    private readonly IUnitOfWork _unitOfwork;

    public AAppointmentConfirmedReceiveIntegrationEventHandler(
        IEmailRequestService emailRequestService,
        IUnitOfWork unitOfwork)
    {
        _emailRequestService = emailRequestService;
        _unitOfwork = unitOfwork;
    }

    public async Task Handle(
        AppointmentConfirmedReceiveIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        var contract = integrationEvent
            .AppointmentConfirmedIntegrationEventContract;

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

        await SendEmailRequestAsync(
            appointment, contract.CorrelationId, cancellationToken);
    }

    private async Task SendEmailRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentConfirmedEmailRequestFactory(
            appointment,
            correlationId,
            Guid.Empty.ToString()
        );

        await _emailRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}