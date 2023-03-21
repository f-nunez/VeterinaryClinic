using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}