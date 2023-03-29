using System.Globalization;

namespace Fnunez.VeterinaryClinic.Public.Web.Services.Language;

public interface ILanguageService
{
    CultureInfo GetCultureInfo(string? language);
    string GetStringLocalizer(string key, string? language);
}