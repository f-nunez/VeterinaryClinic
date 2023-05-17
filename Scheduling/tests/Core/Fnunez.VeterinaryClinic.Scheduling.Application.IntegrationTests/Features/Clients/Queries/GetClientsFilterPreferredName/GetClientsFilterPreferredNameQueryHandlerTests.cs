using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Clients.Queries.GetClientsFilterPreferredName;

[Collection(nameof(TestContextFixture))]
public class GetClientsFilterPreferredNameQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientsFilterPreferredNameQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientsFilterPreferredNameRequest();

        var query = new GetClientsFilterPreferredNameQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientsFilterPreferredNameResponse()
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

        var request = new GetClientsFilterPreferredNameRequest
        {
            PreferredNameFilterValue = preferredName
        };

        var query = new GetClientsFilterPreferredNameQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientsFilterPreferredNameResponse>(actual);

        Assert.NotNull(actual.ClientPreferredNames);

        Assert.NotEmpty(actual.ClientPreferredNames);

        Assert.StartsWith(preferredName, actual.ClientPreferredNames.FirstOrDefault());
    }
}