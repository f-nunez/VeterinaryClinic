using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

public class FileSystemDeleterService : IFileSystemDeleterService
{
    public void Delete(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}