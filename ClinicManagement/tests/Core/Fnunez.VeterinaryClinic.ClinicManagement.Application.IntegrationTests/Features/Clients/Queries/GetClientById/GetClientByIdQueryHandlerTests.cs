using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Queries.GetClientById;

[Collection(nameof(TestContextFixture))]
public class GetClientByIdQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientByIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientByIdRequest();

        var query = new GetClientByIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientByIdResponse()
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

        var request = new GetClientByIdRequest
        {
            Id = client.Id
        };

        var query = new GetClientByIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientByIdResponse>(actual);

        Assert.NotNull(actual.Client);

        Assert.Equal(emailAddress, actual.Client.EmailAddress);

        Assert.Equal(fullName, actual.Client.FullName);

        Assert.True(actual.Client.IsActive);

        Assert.Equal(preferredDoctorId, actual.Client.PreferredDoctorId);

        Assert.Equal((int)preferredLanguage, actual.Client.PreferredLanguage);

        Assert.Equal(preferredName, actual.Client.PreferredName);

        Assert.Equal(salutation, actual.Client.Salutation);
    }
}