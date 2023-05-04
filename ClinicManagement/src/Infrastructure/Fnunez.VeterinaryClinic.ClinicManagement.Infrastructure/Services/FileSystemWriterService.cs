using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

public class FileSystemWriterService : IFileSystemWriterService
{
    public async Task WriteAsync(byte[] fileData, string filePath)
    {
        if (fileData is null)
            throw new ArgumentNullException(nameof(fileData));

        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException($"{nameof(filePath)} is empty.");

        var memoryStream = new MemoryStream(fileData);

        if (memoryStream.Length <= 0)
            throw new ArgumentNullException(nameof(memoryStream));

        var directory = Path.GetDirectoryName(filePath);

        if (string.IsNullOrEmpty(directory))
            throw new ArgumentNullException(directory);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        using (var stream = new FileStream(filePath, FileMode.Create))
            await memoryStream.CopyToAsync(stream);
    }
}