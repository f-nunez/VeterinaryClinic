using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Doctors.Commands.DeleteDoctor;

[Collection(nameof(TestContextFixture))]
public class DeleteDoctorCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public DeleteDoctorCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new DeleteDoctorRequest();

        var command = new DeleteDoctorCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsDeleteDoctorResponse()
    {
        // Arrange
        var fullName = "a";

        var doctor = new Doctor(fullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var request = new DeleteDoctorRequest { Id = doctor.Id };

        var command = new DeleteDoctorCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<DeleteDoctorResponse>(actual);

        var deletedDoctor = await _fixture.GetByIdAsync<Doctor>(doctor.Id);

        Assert.NotNull(deletedDoctor);

        Assert.False(deletedDoctor.IsActive);
    }
}