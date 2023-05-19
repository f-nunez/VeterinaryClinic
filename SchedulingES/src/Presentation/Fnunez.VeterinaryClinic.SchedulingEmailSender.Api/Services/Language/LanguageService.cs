using System.Globalization;
using System.Reflection;
using System.Resources;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.Language;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.Language;

public class LanguageService : ILanguageService
{
    private const string EnglishLanguage = "English";
    private const string SpanishLanguage = "Spanish";

    private const string EsMxCultureName = "es-MX";
    private const string EnUsCultureName = "en-US";

    private const string ResourcesFileName = "Strings";
    private const string ResourcesNamespace = "Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.Language.Resources";

    private readonly ILogger<LanguageService> _logger;
    private readonly ResourceManager _resourceManager;

    public LanguageService(ILogger<LanguageService> logger)
    {
        _logger = logger;

        _resourceManager = new ResourceManager(
            $"{ResourcesNamespace}.{ResourcesFileName}",
            Assembly.GetExecutingAssembly()
        );
    }

    public CultureInfo GetCultureInfo(string? language)
    {
        if (string.IsNullOrEmpty(language))
            return new CultureInfo(EnUsCultureName);

        switch (language)
        {
            case EnglishLanguage:
                return new CultureInfo(EnUsCultureName);
            case SpanishLanguage:
                return new CultureInfo(EsMxCultureName);
            default:
                return new CultureInfo(EnUsCultureName);
        }
    }

    public string GetStringLocalizer(string key, string? language)
    {
        try
        {
            CultureInfo cultureInfo = GetCultureInfo(language);

            return _resourceManager.GetString(key, cultureInfo) ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return key;
        }
    }
}