using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.AppointmentTypes.Commands.UpdateAppointmentType;

[Collection(nameof(TestContextFixture))]
public class UpdateAppointmentTypeCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdateAppointmentTypeCommandHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdateAppointmentTypeRequest();

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdateAppointmentTypeResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        code = "codeX";

        duration = 120;

        name = "nameX";

        var updateRequest = new UpdateAppointmentTypeRequest
        {
            Code = code,
            Duration = duration,
            Id = appointmentType.Id,
            Name = name
        };

        var updateCommand = new UpdateAppointmentTypeCommand(updateRequest);

        // Act
        var actual = await _fixture.SendAsync(updateCommand);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdateAppointmentTypeResponse>(actual);

        Assert.NotNull(actual.AppointmentType);

        Assert.Equal(code, actual.AppointmentType.Code);

        Assert.Equal(duration, actual.AppointmentType.Duration);

        Assert.Equal(name, actual.AppointmentType.Name);
    }
}