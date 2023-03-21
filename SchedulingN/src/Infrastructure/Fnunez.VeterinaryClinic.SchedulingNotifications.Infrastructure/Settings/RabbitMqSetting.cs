using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}