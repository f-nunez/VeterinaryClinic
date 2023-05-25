using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

public class FileSystemDeleterService : IFileSystemDeleterService
{
    public void Delete(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException($"{nameof(filePath)} is empty.");

        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}