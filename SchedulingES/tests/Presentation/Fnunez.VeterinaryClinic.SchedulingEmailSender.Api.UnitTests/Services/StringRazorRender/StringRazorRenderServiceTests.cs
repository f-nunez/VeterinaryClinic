using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.UnitTests.Services.StringRazorRender;

public class StringRazorRenderServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task RenderRazorToStringAsync_ViewNameIsEmpty_ThrowsArgumentException(
        string viewName)
    {
        // Arrange
        var expectedErrorMessage = $"{viewName} is empty.";

        var viewModel = new AppointmentConfirmedPayload();

        var mockIRazorViewEngine = new Mock<IRazorViewEngine>();

        var mockIServiceProvider = new Mock<IServiceProvider>();

        var mockITempDataProvider = new Mock<ITempDataProvider>();

        var renderService = new StringRazorRenderService
        (
            mockIRazorViewEngine.Object,
            mockIServiceProvider.Object,
            mockITempDataProvider.Object
        );

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            renderService.RenderRazorToStringAsync(viewName, viewModel));

        // Assert
        Assert.NotNull(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}