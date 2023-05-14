using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Clients.Queries.GetClientsFilterPreferredDoctor;

[Collection(nameof(TestContextFixture))]
public class GetClientsFilterPreferredDoctorQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetClientsFilterPreferredDoctorQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetClientsFilterPreferredDoctorRequest();

        var query = new GetClientsFilterPreferredDoctorQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetClientsFilterPreferredDoctorResponse()
    {
        // Arrange
        var doctorFullName = "doc";

        var doctor = new Doctor(doctorFullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var take = 1;

        var request = new GetClientsFilterPreferredDoctorRequest
        {
            DataGridRequest = new DataGridRequest
            {
                Search = doctorFullName,
                Take = take
            }
        };

        var query = new GetClientsFilterPreferredDoctorQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetClientsFilterPreferredDoctorResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.StartsWith(doctorFullName, actual.DataGridResponse.Items.FirstOrDefault()?.FullName);
    }
}