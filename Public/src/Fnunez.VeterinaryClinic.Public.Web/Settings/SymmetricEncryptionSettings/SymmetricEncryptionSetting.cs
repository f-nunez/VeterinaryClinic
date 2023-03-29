namespace Fnunez.VeterinaryClinic.Public.Web.Settings;

public class SymmetricEncryptionSetting : ISymmetricEncryptionSetting
{
    public string HashAlgorithmName { get; set; } = null!;
    public int Iterations { get; set; }
    public int OutputLength { get; set; }
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
}