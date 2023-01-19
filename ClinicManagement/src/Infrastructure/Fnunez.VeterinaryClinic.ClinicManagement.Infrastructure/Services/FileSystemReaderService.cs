using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

public class FileSystemReaderService : IFileSystemReaderService
{
    public async Task<byte[]> ReadAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException($"{nameof(filePath)} is empty.");

        if (!File.Exists(filePath))
            throw new ArgumentException($"{filePath} not found.");

        var memoryStream = new MemoryStream();

        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            await fileStream.CopyToAsync(memoryStream);

        memoryStream.Position = default;

        return memoryStream.ToArray();
    }
}