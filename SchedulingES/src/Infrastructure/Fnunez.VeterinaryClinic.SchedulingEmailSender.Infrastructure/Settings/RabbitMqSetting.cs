using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}