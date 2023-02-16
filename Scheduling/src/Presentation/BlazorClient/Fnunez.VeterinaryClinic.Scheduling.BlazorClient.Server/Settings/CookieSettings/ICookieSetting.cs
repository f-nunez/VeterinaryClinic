namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

public interface ICookieSetting
{
    public string AuthenticationScheme { get; }
    public double ExpireTimeInMinutes { get; }
    public string Name { get; }
    public SameSiteMode SameSiteMode { get; }
}