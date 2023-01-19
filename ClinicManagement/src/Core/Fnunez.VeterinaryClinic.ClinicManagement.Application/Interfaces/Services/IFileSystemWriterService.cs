namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;

public interface IFileSystemWriterService
{
    public Task WriteAsync(byte[] fileData, string filePath);
}