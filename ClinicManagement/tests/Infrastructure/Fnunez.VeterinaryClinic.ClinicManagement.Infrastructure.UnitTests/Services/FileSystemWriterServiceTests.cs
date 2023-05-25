using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.UnitTests.Services;

public class FileSystemWriterServiceTests
{
    [Theory]
    [InlineData(new byte[0])]
    [InlineData(null)]
    public async Task WriteAsync_FileDataIsEmpty_ThrowsArgumentNullException(
        byte[] fileData)
    {
        // Arrange
        var filePath = "a";

        IFileSystemWriterService fileSystemWriterService;

        fileSystemWriterService = new FileSystemWriterService();

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            fileSystemWriterService.WriteAsync(fileData, filePath));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentNullException>(actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task WriteAsync_FilePathIsEmpty_ThrowsArgumentException(
        string filePath)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(filePath)} is empty.";

        var fileData = new byte[] { 0x20 };

        IFileSystemWriterService fileSystemWriterService;

        fileSystemWriterService = new FileSystemWriterService();

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            fileSystemWriterService.WriteAsync(fileData, filePath));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}