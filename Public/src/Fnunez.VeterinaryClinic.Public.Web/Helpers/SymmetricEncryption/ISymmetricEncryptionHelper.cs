namespace Fnunez.VeterinaryClinic.Public.Web.Helpers.SymmetricEncryption;

public interface ISymmetricEncryptionHelper
{
    Task<string> DecryptFromBase64Async(string stringToDecrypt);
    Task<string> EncryptToBase64Async(string stringToEncrypt);
}