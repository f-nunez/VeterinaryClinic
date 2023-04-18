namespace Fnunez.VeterinaryClinic.Identity.Infrastructure.Settings;

public class IdentityServerSetting
{
    public bool EmitStaticAudienceClaim { get; set; }
    public string IssuerUri { get; set; } = null!;
    public bool RaiseErrorEvents { get; set; }
    public bool RaiseFailureEvents { get; set; }
    public bool RaiseInformationEvents { get; set; }
    public bool RaiseSuccessEvents { get; set; }
}