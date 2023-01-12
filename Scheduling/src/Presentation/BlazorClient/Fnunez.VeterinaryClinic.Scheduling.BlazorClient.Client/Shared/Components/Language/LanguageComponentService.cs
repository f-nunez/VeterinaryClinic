namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.Language;

public class LanguageComponentService : ILanguageComponentService
{
    private Language _language { set; get; } = null!;
    private readonly ILanguageComponentData _languageComponentData;

    public LanguageComponentService(ILanguageComponentData languageComponentData)
    {
        _languageComponentData = languageComponentData;
    }

    public Language GetLanguage(string cultureCode)
    {
        Language? language = _languageComponentData.Languages
            .FirstOrDefault(l => l.CultureCode == cultureCode);

        if (language != null)
            return language;

        throw new ArgumentNullException(nameof(language));
    }

    public IReadOnlyList<Language> GetLanguages()
    {
        return _languageComponentData.Languages;
    }

    public void SetLanguage(string cultureCode)
    {
        Language? language = _languageComponentData.Languages
            .FirstOrDefault(l => l.CultureCode == cultureCode);

        if (language is null)
            throw new ArgumentException($"Culture: ({cultureCode}) not found. Please clean your Browser's cache.");

        _language = language;
    }
}