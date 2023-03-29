namespace Fnunez.VeterinaryClinic.Public.Web.Settings;

public class RabbitMqSetting : IRabbitMqSetting
{
    public Uri HostAddress { get; set; } = null!;
}