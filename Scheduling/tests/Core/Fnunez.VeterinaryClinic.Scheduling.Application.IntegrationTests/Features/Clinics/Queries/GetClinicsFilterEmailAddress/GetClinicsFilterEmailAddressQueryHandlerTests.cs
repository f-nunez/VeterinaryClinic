using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

[Collection(nameof(TestContextFixture))]
public class GetClinicsFilterEmailAddressQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClinicsFilterEmailAddressQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClinicsFilterEmailAddressRequest();

        var query = new GetClinicsFilterEmailAddressQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClinicsFilterEmailAddressResponse()
    {
        // Arrange
        var address = "a";

        var emailAddress = "test@nunez.ninja";

        var name = "b";

        var clinic = new Clinic(address, emailAddress, name);

        await _fixture.AddAsync<Clinic>(clinic);

        var request = new GetClinicsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddress
        };

        var query = new GetClinicsFilterEmailAddressQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClinicsFilterEmailAddressResponse>(actual);

        Assert.NotNull(actual.ClinicEmailAddresses);

        Assert.NotEmpty(actual.ClinicEmailAddresses);

        Assert.StartsWith(emailAddress, actual.ClinicEmailAddresses.FirstOrDefault());
    }
}