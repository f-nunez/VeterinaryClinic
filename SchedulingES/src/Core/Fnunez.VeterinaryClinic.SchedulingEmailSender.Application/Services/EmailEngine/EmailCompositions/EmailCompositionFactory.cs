using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.Language;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;

public class EmailCompositionFactory : IEmailCompositionFactory
{
    private readonly ILanguageService _languageService;
    private readonly IStringRazorRenderService _stringRazorRenderService;

    public EmailCompositionFactory(
        ILanguageService languageService,
        IStringRazorRenderService stringRazorRenderService)
    {
        _languageService = languageService;
        _stringRazorRenderService = stringRazorRenderService;
    }

    public async Task<EmailComposition> GetEmailCompositionAsync(
        EmailEvent emailEvent,
        BasePayload payload)
    {
        var body = string.Empty;
        bool isHtmlBody = default;
        var subject = string.Empty;

        switch (emailEvent)
        {
            case EmailEvent.AppointmentConfirmed:
                body = await _stringRazorRenderService.RenderRazorToStringAsync(
                    "Views/EmailTemplates/AppointmentConfirmed.cshtml",
                    payload as AppointmentConfirmedPayload
                );
                isHtmlBody = true;
                subject = _languageService.GetStringLocalizer(
                    "AppointmentConfirmed_Subject",
                    payload.Language
                );
                break;
            case EmailEvent.AppointmentCreated:
                body = await _stringRazorRenderService.RenderRazorToStringAsync(
                    "Views/EmailTemplates/AppointmentCreated.cshtml",
                    payload as AppointmentCreatedPayload
                );
                isHtmlBody = true;
                subject = _languageService.GetStringLocalizer(
                    "AppointmentCreated_Subject",
                    payload.Language
                );
                break;
            case EmailEvent.AppointmentDeleted:
                body = await _stringRazorRenderService.RenderRazorToStringAsync(
                    "Views/EmailTemplates/AppointmentDeleted.cshtml",
                    payload as AppointmentDeletedPayload
                );
                isHtmlBody = true;
                subject = _languageService.GetStringLocalizer(
                    "AppointmentDeleted_Subject",
                    payload.Language
                );
                break;
            case EmailEvent.AppointmentUpdated:
                body = await _stringRazorRenderService.RenderRazorToStringAsync(
                    "Views/EmailTemplates/AppointmentUpdated.cshtml",
                    payload as AppointmentUpdatedPayload
                );
                isHtmlBody = true;
                subject = _languageService.GetStringLocalizer(
                    "AppointmentUpdated_Subject",
                    payload.Language
                );
                break;
            default:
                throw new ArgumentException(
                    $"{nameof(emailEvent)} not found with value: {emailEvent}");
        }

        return new EmailComposition
        {
            Body = body,
            IsBodyHtml = isHtmlBody,
            Subject = subject,
            To = payload.SendTo
        };
    }
}