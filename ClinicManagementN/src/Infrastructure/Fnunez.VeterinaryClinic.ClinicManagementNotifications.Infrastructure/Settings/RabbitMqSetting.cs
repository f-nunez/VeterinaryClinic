using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}