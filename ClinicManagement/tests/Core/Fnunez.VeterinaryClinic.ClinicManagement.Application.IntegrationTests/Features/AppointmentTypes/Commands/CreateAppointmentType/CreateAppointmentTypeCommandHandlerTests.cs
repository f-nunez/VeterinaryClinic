using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.AppointmentTypes.Commands.CreateAppointmentType;

[Collection(nameof(TestContextFixture))]
public class CreateAppointmentTypeCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public CreateAppointmentTypeCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new CreateAppointmentTypeRequest();

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsCreateAppointmentTypeResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var request = new CreateAppointmentTypeRequest
        {
            Code = code,
            Duration = duration,
            Name = name
        };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<CreateAppointmentTypeResponse>(actual);

        Assert.NotNull(actual.AppointmentType);

        Assert.Equal(code, actual.AppointmentType.Code);

        Assert.Equal(duration, actual.AppointmentType.Duration);

        Assert.Equal(name, actual.AppointmentType.Name);
    }
}