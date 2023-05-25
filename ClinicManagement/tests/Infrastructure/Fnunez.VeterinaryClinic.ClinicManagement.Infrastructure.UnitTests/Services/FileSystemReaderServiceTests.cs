using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.UnitTests.Services;

public class FileSystemReaderServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task ReadAsync_FilePathIsEmpty_ThrowsArgumentException(
        string filePath)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(filePath)} is empty.";

        IFileSystemReaderService fileSystemDeleterService;

        fileSystemDeleterService = new FileSystemReaderService();

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            fileSystemDeleterService.ReadAsync(filePath));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}