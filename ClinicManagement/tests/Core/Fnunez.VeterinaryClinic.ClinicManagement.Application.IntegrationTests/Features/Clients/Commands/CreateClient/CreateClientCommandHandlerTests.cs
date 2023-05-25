using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Commands.CreateClient;

[Collection(nameof(TestContextFixture))]
public class CreateClientCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public CreateClientCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new CreateClientRequest();

        var command = new CreateClientCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsCreateClientResponse()
    {
        // Arrange
        var emailAddress = "test@nunez.ninja";

        var fullName = "a";

        int? preferredDoctorId = null;

        var preferredLanguage = (int)PreferredLanguage.English;

        var preferredName = "b";

        var salutation = "Mister a";


        var request = new CreateClientRequest
        {
            EmailAddress = emailAddress,
            FullName = fullName,
            PreferredDoctorId = preferredDoctorId,
            PreferredLanguage = preferredLanguage,
            PreferredName = preferredName,
            Salutation = salutation
        };

        var command = new CreateClientCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<CreateClientResponse>(actual);

        Assert.NotNull(actual.Client);

        Assert.Equal(emailAddress, actual.Client.EmailAddress);

        Assert.Equal(fullName, actual.Client.FullName);

        Assert.Equal(preferredDoctorId, actual.Client.PreferredDoctorId);

        Assert.Equal(preferredLanguage, actual.Client.PreferredLanguage);

        Assert.Equal(preferredName, actual.Client.PreferredName);

        Assert.Equal(salutation, actual.Client.Salutation);
    }
}