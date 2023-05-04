namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

public interface ISymmetricEncryptionSetting
{
    string HashAlgorithmName { get; }
    int Iterations { get; }
    int OutputLength { get; }
    string Password { get; }
    string Salt { get; }
}