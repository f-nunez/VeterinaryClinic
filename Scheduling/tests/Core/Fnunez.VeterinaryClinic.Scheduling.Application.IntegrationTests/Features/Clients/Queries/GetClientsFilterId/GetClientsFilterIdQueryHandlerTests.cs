using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Clients.Queries.GetClientsFilterId;

[Collection(nameof(TestContextFixture))]
public class GetClientsFilterIdQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientsFilterIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientsFilterIdRequest();

        var query = new GetClientsFilterIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientsFilterIdResponse()
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

        var request = new GetClientsFilterIdRequest
        {
            IdFilterValue = client.Id.ToString()
        };

        var query = new GetClientsFilterIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientsFilterIdResponse>(actual);

        Assert.NotNull(actual.ClientIds);

        Assert.NotEmpty(actual.ClientIds);

        Assert.StartsWith(client.Id.ToString(), actual.ClientIds.FirstOrDefault());
    }
}