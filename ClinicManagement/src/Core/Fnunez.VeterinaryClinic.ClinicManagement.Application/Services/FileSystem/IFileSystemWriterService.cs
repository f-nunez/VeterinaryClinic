namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;

public interface IFileSystemWriterService
{
    public Task WriteAsync(byte[] fileData, string filePath);
}