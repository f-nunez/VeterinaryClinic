using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clinics.Commands.UpdateClinic;

[Collection(nameof(TestContextFixture))]
public class UpdateClinicCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdateClinicCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdateClinicRequest();

        var command = new UpdateClinicCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdateClinicResponse()
    {
        // Arrange
        var address = "a";

        var emailAddress = "test@nunez.ninja";

        var name = "b";

        var clinic = new Clinic(address, emailAddress, name);

        await _fixture.AddAsync<Clinic>(clinic);

        address = "aa";

        emailAddress = "test2@nunez.ninja";

        name = "bb";

        var request = new UpdateClinicRequest
        {
            Address = address,
            EmailAddress = emailAddress,
            Id = clinic.Id,
            Name = name
        };

        var command = new UpdateClinicCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdateClinicResponse>(actual);

        Assert.NotNull(actual.Clinic);

        Assert.Equal(address, actual.Clinic.Address);

        Assert.Equal(emailAddress, actual.Clinic.EmailAddress);

        Assert.Equal(name, actual.Clinic.Name);
    }
}