namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;

public interface ISymmetricEncryptionHelper
{
    Task<string> DecryptFromBase64Async(string stringToDecrypt);
    Task<string> EncryptToBase64Async(string stringToEncrypt);
}