namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

public class EmailTemplateSetting : IEmailTemplateSetting
{
    public string UrlQueryToConfirmAppointment { get; set; } = null!;
}