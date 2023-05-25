using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Clinics.Queries.GetClinics;

[Collection(nameof(TestContextFixture))]
public class GetClinicsQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClinicsQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClinicsRequest();

        var query = new GetClinicsQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClinicsResponse()
    {
        // Arrange
        var address = "a";

        var emailAddress = "test@nunez.ninja";

        var name = "b";

        var take = 1;

        var clinic = new Clinic(address, emailAddress, name);

        await _fixture.AddAsync<Clinic>(clinic);

        var request = new GetClinicsRequest
        {
            DataGridRequest = new DataGridRequest { Take = take }
        };

        var query = new GetClinicsQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClinicsResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.Equal(take, actual.DataGridResponse.Items.Count);
    }
}