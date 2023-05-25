using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.UnitTests.Helpers.SymmetricEncryption;

public class SymmetricEncryptionHelperTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task DecryptFromBase64Async_StringToDecryptIsEmpty_ThrowsArgumentexception(
        string stringToDecrypt)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(stringToDecrypt)} is empty.";

        var mockISymmetricEncryptionSetting = new Mock<ISymmetricEncryptionSetting>();

        var encryptionHelper = new SymmetricEncryptionHelper(
            mockISymmetricEncryptionSetting.Object);

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            encryptionHelper.DecryptFromBase64Async(stringToDecrypt));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Fact]
    public async Task DecryptFromBase64Async_StringToDecrypt_ReturnsDecryptedString()
    {
        // Arrange
        var stringToDecrypt = "/nIF5S+auGDQr+PM6rNamA==";

        var expectedString = "stringToEncrypt";

        var mockISymmetricEncryptionSetting = new Mock<ISymmetricEncryptionSetting>();

        mockISymmetricEncryptionSetting.Setup(x => x.HashAlgorithmName).Returns("SHA256");

        mockISymmetricEncryptionSetting.Setup(x => x.Iterations).Returns(100);

        mockISymmetricEncryptionSetting.Setup(x => x.OutputLength).Returns(16);

        mockISymmetricEncryptionSetting.Setup(x => x.Password).Returns("Password");

        mockISymmetricEncryptionSetting.Setup(x => x.Salt).Returns("Salt");

        var encryptionHelper = new SymmetricEncryptionHelper(
            mockISymmetricEncryptionSetting.Object);

        // Act
        var actual = await encryptionHelper.DecryptFromBase64Async(stringToDecrypt);

        // Assert
        Assert.Equal(expectedString, actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task EncryptToBase64Async_StringToEncryptIsEmpty_ThrowsArgumentexception(
        string stringToEncrypt)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(stringToEncrypt)} is empty.";

        var mockISymmetricEncryptionSetting = new Mock<ISymmetricEncryptionSetting>();

        var encryptionHelper = new SymmetricEncryptionHelper(
            mockISymmetricEncryptionSetting.Object);

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            encryptionHelper.EncryptToBase64Async(stringToEncrypt));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Fact]
    public async Task EncryptToBase64Async_StringToEncrypt_ReturnsEncryptedString()
    {
        // Arrange
        var stringToEncrypt = "stringToEncrypt";

        var expectedEncryptedString = "/nIF5S+auGDQr+PM6rNamA==";

        var mockISymmetricEncryptionSetting = new Mock<ISymmetricEncryptionSetting>();

        mockISymmetricEncryptionSetting.Setup(x => x.HashAlgorithmName).Returns("SHA256");

        mockISymmetricEncryptionSetting.Setup(x => x.Iterations).Returns(100);

        mockISymmetricEncryptionSetting.Setup(x => x.OutputLength).Returns(16);

        mockISymmetricEncryptionSetting.Setup(x => x.Password).Returns("Password");

        mockISymmetricEncryptionSetting.Setup(x => x.Salt).Returns("Salt");

        var encryptionHelper = new SymmetricEncryptionHelper(
            mockISymmetricEncryptionSetting.Object);

        // Act
        var actual = await encryptionHelper.EncryptToBase64Async(stringToEncrypt);

        // Assert
        Assert.Equal(expectedEncryptedString, actual);
    }
}