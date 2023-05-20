using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Services;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.UnitTests.Services;

public class FileSystemDeleterServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Delete_FilePathIsEmpty_ThrowsArgumentException(
        string filePath)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(filePath)} is empty.";

        IFileSystemDeleterService fileSystemDeleterService;

        fileSystemDeleterService = new FileSystemDeleterService();

        // Act
        var actual = Assert.Throws<ArgumentException>(() =>
            fileSystemDeleterService.Delete(filePath));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}