using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Doctors.Commands.UpdateDoctor;

[Collection(nameof(TestContextFixture))]
public class UpdateDoctorCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdateDoctorCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdateDoctorRequest();

        var command = new UpdateDoctorCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdateDoctorResponse()
    {
        // Arrange
        var fullName = "a";

        var doctor = new Doctor(fullName);

        await _fixture.AddAsync<Doctor>(doctor);

        fullName = "aa";

        var request = new UpdateDoctorRequest
        {
            FullName = fullName,
            Id = doctor.Id
        };

        var command = new UpdateDoctorCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdateDoctorResponse>(actual);

        Assert.NotNull(actual.Doctor);

        Assert.Equal(fullName, actual.Doctor.FullName);
    }
}