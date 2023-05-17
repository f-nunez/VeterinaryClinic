using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentTypesFilterDurationQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentTypesFilterDurationQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentTypesFilterDurationRequest();

        var query = new GetAppointmentTypesFilterDurationQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentTypesFilterDurationResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var request = new GetAppointmentTypesFilterDurationRequest
        {
            DurationFilterValue = duration.ToString()
        };

        var query = new GetAppointmentTypesFilterDurationQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentTypesFilterDurationResponse>(actual);

        Assert.NotNull(actual.AppointmentTypeDurations);

        Assert.NotEmpty(actual.AppointmentTypeDurations);

        Assert.StartsWith(duration.ToString(), actual.AppointmentTypeDurations.FirstOrDefault());
    }
}