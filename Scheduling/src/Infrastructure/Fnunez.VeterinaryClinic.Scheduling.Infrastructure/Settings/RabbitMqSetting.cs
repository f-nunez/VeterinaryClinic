using Fnunez.VeterinaryClinic.Scheduling.Application.Settings;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}