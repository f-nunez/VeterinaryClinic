namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Language;

public interface ILanguageComponentService
{
    public Language GetLanguage(string cultureCode);
    public IReadOnlyList<Language> GetLanguages();
    public void SetLanguage(string cultureCode);
}