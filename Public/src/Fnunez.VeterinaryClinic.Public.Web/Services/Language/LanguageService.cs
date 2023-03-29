using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Fnunez.VeterinaryClinic.Public.Web.Services.Language;

public class LanguageService : ILanguageService
{
    private const string EnglishLanguage = "English";
    private const string SpanishLanguage = "Spanish";

    private const string EsMxCultureInfo = "es-MX";
    private const string EnUsCultureInfo = "en-US";

    private const string ResourcesFileName = "Strings";
    private const string ResourcesNamespace = "Fnunez.VeterinaryClinic.Public.Web.Services.Language.Resources";

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
            return new CultureInfo(EnUsCultureInfo);

        switch (language)
        {
            case EnglishLanguage:
                return new CultureInfo(EnUsCultureInfo);
            case SpanishLanguage:
                return new CultureInfo(EsMxCultureInfo);
            default:
                return new CultureInfo(EnUsCultureInfo);
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