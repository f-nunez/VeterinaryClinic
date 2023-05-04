namespace Fnunez.VeterinaryClinic.EmailService.Api.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}