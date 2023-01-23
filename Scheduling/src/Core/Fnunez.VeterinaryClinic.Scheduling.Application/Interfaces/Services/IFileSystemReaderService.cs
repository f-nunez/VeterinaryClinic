namespace Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Services;

public interface IFileSystemReaderService
{
    public Task<byte[]> ReadAsync(string filePath);
}