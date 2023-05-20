using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentTypesFilterIdQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentTypesFilterIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentTypesFilterIdRequest();

        var query = new GetAppointmentTypesFilterIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentTypesFilterIdResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var request = new GetAppointmentTypesFilterIdRequest
        {
            IdFilterValue = appointmentType.Id.ToString()
        };

        var query = new GetAppointmentTypesFilterIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentTypesFilterIdResponse>(actual);

        Assert.NotNull(actual.AppointmentTypeIds);

        Assert.NotEmpty(actual.AppointmentTypeIds);

        Assert.StartsWith(appointmentType.Id.ToString(), actual.AppointmentTypeIds.FirstOrDefault());
    }
}