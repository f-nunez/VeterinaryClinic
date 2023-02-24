namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;

public interface IFileSystemReaderService
{
    public Task<byte[]> ReadAsync(string filePath);
}