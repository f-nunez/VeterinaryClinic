using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}