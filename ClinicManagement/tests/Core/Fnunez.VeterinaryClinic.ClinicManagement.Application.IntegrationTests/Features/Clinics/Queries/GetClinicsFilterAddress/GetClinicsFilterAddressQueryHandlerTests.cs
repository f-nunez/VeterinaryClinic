using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clinics.Queries.GetClinicsFilterAddress;

[Collection(nameof(TestContextFixture))]
public class GetClinicsFilterAddressQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClinicsFilterAddressQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClinicsFilterAddressRequest();

        var query = new GetClinicsFilterAddressQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClinicsFilterAddressResponse()
    {
        // Arrange
        var address = "a";

        var emailAddress = "test@nunez.ninja";

        var name = "b";

        var clinic = new Clinic(address, emailAddress, name);

        await _fixture.AddAsync<Clinic>(clinic);

        var request = new GetClinicsFilterAddressRequest
        {
            AddressFilterValue = address
        };

        var query = new GetClinicsFilterAddressQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClinicsFilterAddressResponse>(actual);

        Assert.NotNull(actual.ClinicAddresses);

        Assert.NotEmpty(actual.ClinicAddresses);

        Assert.StartsWith(address, actual.ClinicAddresses.FirstOrDefault());
    }
}