using System.Web;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.EmailTemplate;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateSetting _emailTemplateSetting;
    private readonly ISymmetricEncryptionHelper _encryptionHelper;

    public EmailTemplateService(
        IEmailTemplateSetting emailTemplateSetting,
        ISymmetricEncryptionHelper encryptionHelper)
    {
        _emailTemplateSetting = emailTemplateSetting;
        _encryptionHelper = encryptionHelper;
    }

    public async Task<string> GetConfirmationUrlAsync(
        Guid appointmentId,
        string? language)
    {
        string encryptedId = await _encryptionHelper
            .EncryptToBase64Async(appointmentId.ToString());

        string encodedEncryptedId = HttpUtility.UrlEncode(encryptedId);

        return string.Format(
            _emailTemplateSetting.UrlQueryToConfirmAppointment,
            encodedEncryptedId,
            language
        );
    }
}