using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Commands.UpdateClient;

[Collection(nameof(TestContextFixture))]
public class UpdateClientCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdateClientCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdateClientRequest();

        var command = new UpdateClientCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdateClientResponse()
    {
        // Arrange
        var emailAddress = "test@nunez.ninja";

        var fullName = "a";

        int? preferredDoctorId = null;

        var preferredLanguage = PreferredLanguage.English;

        var preferredName = "b";

        var salutation = "Mister a";

        var client = new Client(
            fullName,
            preferredName,
            salutation,
            emailAddress,
            preferredLanguage,
            preferredDoctorId
        );

        await _fixture.AddAsync<Client>(client);

        emailAddress = "test2@nunez.ninja";

        fullName = "b";

        preferredDoctorId = 1;

        preferredLanguage = PreferredLanguage.Spanish;

        preferredName = "c";

        salutation = "Mister b";

        var request = new UpdateClientRequest
        {
            ClientId = client.Id,
            EmailAddress = emailAddress,
            FullName = fullName,
            PreferredDoctorId = preferredDoctorId,
            PreferredLanguage = (int)preferredLanguage,
            PreferredName = preferredName,
            Salutation = salutation
        };

        var command = new UpdateClientCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdateClientResponse>(actual);

        Assert.NotNull(actual.Client);

        Assert.Equal(emailAddress, actual.Client.EmailAddress);

        Assert.Equal(fullName, actual.Client.FullName);

        Assert.Equal(preferredDoctorId, actual.Client.PreferredDoctorId);

        Assert.Equal((int)preferredLanguage, actual.Client.PreferredLanguage);

        Assert.Equal(preferredName, actual.Client.PreferredName);

        Assert.Equal(salutation, actual.Client.Salutation);
    }
}