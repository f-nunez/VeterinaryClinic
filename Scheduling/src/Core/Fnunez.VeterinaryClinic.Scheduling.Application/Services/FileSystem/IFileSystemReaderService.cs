namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services;

public interface IFileSystemReaderService
{
    public Task<byte[]> ReadAsync(string filePath);
}