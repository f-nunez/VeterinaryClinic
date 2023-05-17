using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentsFilterAppointmentTypeQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentsFilterAppointmentTypeQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentsFilterAppointmentTypeRequest();

        var query = new GetAppointmentsFilterAppointmentTypeQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentsResponse()
    {
        // Arrange
        var appointmentTypeCode = "code";

        var appointmentTypeDuration = 60;

        var appointmentTypeName = "name";

        var appointmentType = new AppointmentType
        (
            appointmentTypeName,
            appointmentTypeCode,
            appointmentTypeDuration
        );

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var take = 1;

        var request = new GetAppointmentsFilterAppointmentTypeRequest
        {
            DataGridRequest = new DataGridRequest
            {
                Take = take
            }
        };

        var query = new GetAppointmentsFilterAppointmentTypeQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentsFilterAppointmentTypeResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.Equal(take, actual.DataGridResponse.Items.Count());
    }
}