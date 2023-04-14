namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

/// <summary>
/// Helper class to run the application in other Environment Stages.
/// </summary>
public interface IDeploymentSetting
{
    public string EnvironmentName { get; }
    public string WellKnownHttpHeaderReplacement { get; }
    public string WellKnownHttpHeaderToReplace { get; }
}