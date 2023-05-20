using System.Globalization;
using Fnunez.VeterinaryClinic.Public.Web.Services.Language;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Public.Web.UnitTests.Services.Language;

public class LanguageServiceTests
{
    [Theory]
    [InlineData("English")]
    [InlineData("Spanish")]
    public void GetCultureInfo_Language_ReturnsCultureInfo(string language)
    {
        // Arrange
        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetCultureInfo(language);

        // Assert
        Assert.IsType<CultureInfo>(actual);
    }

    [Fact]
    public void GetCultureInfo_LanguageAsEnglish_ReturnsCultureInfo()
    {
        // Arrange
        var language = "English";

        var expectedCultureName = "en-US";

        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetCultureInfo(language);

        // Assert
        Assert.IsType<CultureInfo>(actual);

        Assert.Equal(expectedCultureName, actual.Name);
    }

    [Fact]
    public void GetCultureInfo_LanguageAsSpanish_ReturnsCultureInfo()
    {
        // Arrange
        var language = "Spanish";

        var expectedCultureName = "es-MX";

        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetCultureInfo(language);

        // Assert
        Assert.IsType<CultureInfo>(actual);

        Assert.Equal(expectedCultureName, actual.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetCultureInfo_LanguageIsEmpty_ReturnsEnglishCultureInfo(string language)
    {
        // Arrange
        var expectedCultureName = "en-US";

        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetCultureInfo(language);

        // Assert
        Assert.IsType<CultureInfo>(actual);

        Assert.Equal(expectedCultureName, actual.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetStringLocalizer_KeyIsEmpty_ReturnsEmptyString(string key)
    {
        // Arrange
        var language = "English";

        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetStringLocalizer(key, language);

        // Assert
        Assert.Equal(key, actual);
    }

    [Theory]
    [InlineData("firstKeyNotFound")]
    [InlineData("secondKeyNotFound")]
    public void GetStringLocalizer_KeyIsNotFound_ReturnsKey(string key)
    {
        // Arrange
        var language = "English";

        var mockILogger = new Mock<ILogger<LanguageService>>();

        var languageService = new LanguageService(mockILogger.Object);

        // Act
        var actual = languageService.GetStringLocalizer(key, language);

        // Assert
        Assert.Equal(key, actual);
    }
}