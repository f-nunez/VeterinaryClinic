namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Settings;

public interface IRabbitMqSetting
{
    public Uri HostAddress { get; }
}