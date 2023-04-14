namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;

/// <summary>
/// Helper class to run the application in other Environment Stages.
/// </summary>
public class DeploymentSetting : IDeploymentSetting
{
    public string EnvironmentName { get; set; } = null!;
    public string WellKnownHttpHeaderReplacement { get; set; } = null!;
    public string WellKnownHttpHeaderToReplace { get; set; } = null!;
}