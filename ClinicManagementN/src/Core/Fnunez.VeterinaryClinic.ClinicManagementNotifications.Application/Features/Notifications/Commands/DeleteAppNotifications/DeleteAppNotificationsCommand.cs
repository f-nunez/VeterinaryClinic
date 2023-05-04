using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public record DeleteAppNotificationsCommand(DeleteAppNotificationsRequest DeleteAppNotificationsRequest)
    : IRequest<DeleteAppNotificationsResponse>;