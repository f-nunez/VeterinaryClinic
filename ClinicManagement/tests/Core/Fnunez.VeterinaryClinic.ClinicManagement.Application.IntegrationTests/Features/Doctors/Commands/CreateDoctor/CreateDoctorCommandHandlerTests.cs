using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Doctors.Commands.CreateDoctor;

[Collection(nameof(TestContextFixture))]
public class CreateDoctorCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public CreateDoctorCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new CreateDoctorRequest();

        var command = new CreateDoctorCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsCreateDoctorResponse()
    {
        // Arrange
        var fullName = "a";

        var request = new CreateDoctorRequest
        {
            FullName = fullName
        };

        var command = new CreateDoctorCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<CreateDoctorResponse>(actual);

        Assert.NotNull(actual.Doctor);

        Assert.Equal(fullName, actual.Doctor.FullName);
    }
}