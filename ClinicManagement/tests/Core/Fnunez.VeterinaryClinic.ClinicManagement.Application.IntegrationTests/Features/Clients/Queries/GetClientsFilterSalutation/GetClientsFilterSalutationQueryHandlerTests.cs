using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Queries.GetClientsFilterSalutation;

[Collection(nameof(TestContextFixture))]
public class GetClientsFilterSalutationQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientsFilterSalutationQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientsFilterSalutationRequest();

        var query = new GetClientsFilterSalutationQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientsFilterSalutationResponse()
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

        var request = new GetClientsFilterSalutationRequest
        {
            SalutationFilterValue = salutation
        };

        var query = new GetClientsFilterSalutationQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientsFilterSalutationResponse>(actual);

        Assert.NotNull(actual.ClientSalutations);

        Assert.NotEmpty(actual.ClientSalutations);

        Assert.StartsWith(salutation, actual.ClientSalutations.FirstOrDefault());
    }
}