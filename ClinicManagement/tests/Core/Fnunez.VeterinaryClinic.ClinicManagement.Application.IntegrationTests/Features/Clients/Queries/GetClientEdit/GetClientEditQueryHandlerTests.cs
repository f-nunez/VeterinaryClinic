using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Queries.GetClientEdit;

[Collection(nameof(TestContextFixture))]
public class GetClientEditQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientEditQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientEditRequest();

        var query = new GetClientEditQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientEditResponse()
    {
        // Arrange
        var emailAddress = "test@nunez.ninja";

        var fullName = "a";

        int? preferredDoctorId = null;

        var preferredLanguage = PreferredLanguage.English;

        var preferredName = "b";

        var salutation = "Mister a";

        var client = new Client
        (
            fullName,
            preferredName,
            salutation,
            emailAddress,
            preferredLanguage,
            preferredDoctorId
        );

        await _fixture.AddAsync<Client>(client);

        var request = new GetClientEditRequest { ClientId = client.Id };

        var query = new GetClientEditQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientEditResponse>(actual);

        Assert.NotNull(actual.Client);

        Assert.Equal(client.Id, actual.Client.ClientId);

        Assert.Equal(client.EmailAddress, actual.Client.EmailAddress);

        Assert.Equal(client.FullName, actual.Client.FullName);

        Assert.Equal(client.IsActive, actual.Client.IsActive);

        Assert.Equal(client.PreferredDoctorId, actual.Client.PreferredDoctorId);

        Assert.Equal((int)client.PreferredLanguage, actual.Client.PreferredLanguage);

        Assert.Equal(client.PreferredName, actual.Client.PreferredName);

        Assert.Equal(client.Salutation, actual.Client.Salutation);
    }
}