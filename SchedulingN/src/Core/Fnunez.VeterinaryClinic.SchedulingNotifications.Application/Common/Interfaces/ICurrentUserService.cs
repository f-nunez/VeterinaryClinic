namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}