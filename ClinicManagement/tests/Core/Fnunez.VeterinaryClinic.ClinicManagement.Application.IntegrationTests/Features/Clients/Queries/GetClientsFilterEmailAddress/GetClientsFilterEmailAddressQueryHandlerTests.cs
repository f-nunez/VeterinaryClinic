using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Queries.GetClientsFilterEmailAddress;

[Collection(nameof(TestContextFixture))]
public class GetClientsFilterEmailAddressQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientsFilterEmailAddressQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientsFilterEmailAddressRequest();

        var query = new GetClientsFilterEmailAddressQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientsFilterEmailAddressResponse()
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

        var request = new GetClientsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddress
        };

        var query = new GetClientsFilterEmailAddressQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientsFilterEmailAddressResponse>(actual);

        Assert.NotNull(actual.ClientEmailAddresses);

        Assert.NotEmpty(actual.ClientEmailAddresses);

        Assert.StartsWith(emailAddress, actual.ClientEmailAddresses.FirstOrDefault());
    }
}