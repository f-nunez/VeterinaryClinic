namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.Language;

public class LanguageComponentData : ILanguageComponentData
{
    public IReadOnlyList<Language> Languages { get; set; } = default!;
}