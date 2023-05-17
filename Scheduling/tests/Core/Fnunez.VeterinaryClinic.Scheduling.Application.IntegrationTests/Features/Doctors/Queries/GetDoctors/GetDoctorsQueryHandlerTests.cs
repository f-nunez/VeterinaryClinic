using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Doctors.Queries.GetDoctors;

[Collection(nameof(TestContextFixture))]
public class GetDoctorsQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetDoctorsQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetDoctorsRequest();

        var query = new GetDoctorsQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetDoctorsResponse()
    {
        // Arrange
        var fullName = "a";

        var take = 1;

        var doctor = new Doctor(fullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var request = new GetDoctorsRequest
        {
            DataGridRequest = new DataGridRequest { Take = take }
        };

        var query = new GetDoctorsQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetDoctorsResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.Equal(take, actual.DataGridResponse.Items.Count);
    }
}