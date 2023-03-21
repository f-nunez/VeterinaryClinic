using FluentValidation;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsQueryValidator
    : AbstractValidator<GetAppNotificationsQuery>
{
    public GetAppNotificationsQueryValidator()
    {
        RuleFor(v => v.GetNotificationsRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip is required.");

        RuleFor(v => v.GetNotificationsRequest.Take)
            .GreaterThan(0).WithMessage("Take is required.")
            .LessThanOrEqualTo(100).WithMessage("Take is required.");
    }
}