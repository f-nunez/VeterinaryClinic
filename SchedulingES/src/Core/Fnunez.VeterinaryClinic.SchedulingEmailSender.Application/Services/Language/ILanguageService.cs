using System.Globalization;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.Language;

public interface ILanguageService
{
    CultureInfo GetCultureInfo(string? language);
    string GetStringLocalizer(string key, string? language);
}