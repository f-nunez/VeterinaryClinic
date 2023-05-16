using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandHandler
    : IRequestHandler<DeleteAppointmentCommand, DeleteAppointmentResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmailRequestService _emailRequestService;
    private readonly INotificationRequestService _notificationRequestService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentCommandHandler(
        ICurrentUserService currentUserService,
        IEmailRequestService emailRequestService,
        INotificationRequestService notificationRequestService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _emailRequestService = emailRequestService;
        _notificationRequestService = notificationRequestService;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentResponse> Handle(
        DeleteAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentRequest request = command.DeleteAppointmentRequest;
        var response = new DeleteAppointmentResponse(request.CorrelationId);

        var specification = new AppointmentSpecification(
            request.AppointmentId);

        var appointmentToDelete = await _unitOfWork
            .Repository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointmentToDelete is null)
            throw new NotFoundException(
                nameof(appointmentToDelete), request.AppointmentId);

        appointmentToDelete.SetUpdatedBy(_currentUserService.UserId);

        await _unitOfWork
            .Repository<Appointment>()
            .DeleteAsync(appointmentToDelete, cancellationToken);

        await _unitOfWork.CommitAsync();

        await SendContractsToServiceBusAsync(
            appointmentToDelete, request.CorrelationId, cancellationToken);

        return response;
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
        var factory = new AppointmentDeletedEmailRequestFactory(
            appointment,
            correlationId,
            _currentUserService.UserId
        );

        await _emailRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }

    private async Task SendNotificationRequestAsync(
        Appointment appointment,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var factory = new AppointmentDeletedNotificationRequestFactory(
            appointment,
            correlationId,
            _currentUserService.UserId
        );

        await _notificationRequestService.CreateAndSendAsync(
            factory, cancellationToken);
    }
}