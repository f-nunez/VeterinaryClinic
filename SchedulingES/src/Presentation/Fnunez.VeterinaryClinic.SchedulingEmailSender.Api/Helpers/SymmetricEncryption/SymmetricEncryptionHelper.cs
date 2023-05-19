using System.Security.Cryptography;
using System.Text;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;

public class SymmetricEncryptionHelper : ISymmetricEncryptionHelper
{
    private readonly ISymmetricEncryptionSetting _setting;

    public SymmetricEncryptionHelper(ISymmetricEncryptionSetting setting)
    {
        _setting = setting;
    }

    public async Task<string> DecryptFromBase64Async(string stringToDecrypt)
    {
        if (string.IsNullOrEmpty(stringToDecrypt))
            throw new ArgumentException($"{nameof(stringToDecrypt)} is empty.");

        using var aes = Aes.Create();

        aes.Key = GetKey();

        aes.IV = GetIV();

        using MemoryStream input = new(
            Convert.FromBase64String(stringToDecrypt));

        using CryptoStream cryptoStream = new(
            input, aes.CreateDecryptor(), CryptoStreamMode.Read);

        using MemoryStream output = new();

        await cryptoStream.CopyToAsync(output);

        return Encoding.UTF8.GetString(output.ToArray());
    }

    public async Task<string> EncryptToBase64Async(string stringToEncrypt)
    {
        using var aes = Aes.Create();

        aes.IV = GetIV();

        aes.Key = GetKey();

        using MemoryStream output = new();

        using CryptoStream cryptoStream = new(
            output, aes.CreateEncryptor(), CryptoStreamMode.Write);

        await cryptoStream.WriteAsync(
            Encoding.UTF8.GetBytes(stringToEncrypt));

        await cryptoStream.FlushFinalBlockAsync();

        return Convert.ToBase64String(output.ToArray());
    }

    private byte[] GetIV()
    {
        return new byte[16];
    }

    private byte[] GetKey()
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(_setting.Password),
            Encoding.UTF8.GetBytes(_setting.Salt),
            _setting.Iterations,
            new HashAlgorithmName(_setting.HashAlgorithmName),
            _setting.OutputLength
        );
    }
}