namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;

public interface IFileSystemReaderService
{
    public Task<byte[]> ReadAsync(string filePath);
}