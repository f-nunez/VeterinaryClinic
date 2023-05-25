using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clinics.Queries.GetClinicById;

[Collection(nameof(TestContextFixture))]
public class GetClinicByIdQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClinicByIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClinicByIdRequest();

        var query = new GetClinicByIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClinicByIdResponse()
    {
        // Arrange
        var address = "a";

        var emailAddress = "test@nunez.ninja";

        var name = "b";

        var clinic = new Clinic(address, emailAddress, name);

        await _fixture.AddAsync<Clinic>(clinic);

        var request = new GetClinicByIdRequest { Id = clinic.Id };

        var query = new GetClinicByIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClinicByIdResponse>(actual);

        Assert.NotNull(actual.Clinic);

        Assert.Equal(address, actual.Clinic.Address);

        Assert.Equal(emailAddress, actual.Clinic.EmailAddress);

        Assert.True(actual.Clinic.IsActive);

        Assert.Equal(name, actual.Clinic.Name);
    }
}