namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Constants;

/// <summary>
/// Class <c>JavaScriptConstant</c> contains properties to help invoking functions from time-helper.js file.
/// </summary>
public class JavaScriptConstant
{
    /// <value>
    /// Property <c>GetLocalTime</c> returns a date as a string value in ISO format.
    /// </value>
    public static string GetLocalTime = "getLocalTime";
    /// <value>
    /// Property <c>GetTimezoneOffset</c> returns difference in minutes between the time on the local computer and Universal Coordinated Time (UTC).
    /// </value>
    public static string GetTimezoneOffset = "getTimezoneOffset";
    /// <value>
    /// Property <c>GetLocalTime</c> returns a date converted to a string using Universal Coordinated Time (UTC).
    /// </value>
    public static string GetUtcTime = "getUtcTime";
}