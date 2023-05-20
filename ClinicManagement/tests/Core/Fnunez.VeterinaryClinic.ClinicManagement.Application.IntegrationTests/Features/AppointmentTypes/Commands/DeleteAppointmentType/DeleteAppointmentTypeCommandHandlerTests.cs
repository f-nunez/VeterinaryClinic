using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.AppointmentTypes.Commands.DeleteAppointmentType;

[Collection(nameof(TestContextFixture))]
public class DeleteAppointmentTypeCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public DeleteAppointmentTypeCommandHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new DeleteAppointmentTypeRequest();

        var command = new DeleteAppointmentTypeCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsDeleteAppointmentTypeResponse()
    {
        // Arrange
        var code = "a";

        var duration = 1;

        var name = "a";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var deleteRequest = new DeleteAppointmentTypeRequest
        {
            Id = appointmentType.Id
        };

        var deleteCommand = new DeleteAppointmentTypeCommand(deleteRequest);

        // Act
        var actual = await _fixture.SendAsync(deleteCommand);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<DeleteAppointmentTypeResponse>(actual);

        var deletedAppointmentType = await _fixture
            .GetByIdAsync<AppointmentType>(appointmentType.Id);

        Assert.NotNull(deletedAppointmentType);

        Assert.False(deletedAppointmentType.IsActive);
    }
}