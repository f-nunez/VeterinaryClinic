namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

public class CookieSetting : ICookieSetting
{
    public string AuthenticationScheme { get; set; } = null!;
    public double ExpireTimeInMinutes { get; set; }
    public string Name { get; set; } = null!;
    public SameSiteMode SameSiteMode { get; set; }
}