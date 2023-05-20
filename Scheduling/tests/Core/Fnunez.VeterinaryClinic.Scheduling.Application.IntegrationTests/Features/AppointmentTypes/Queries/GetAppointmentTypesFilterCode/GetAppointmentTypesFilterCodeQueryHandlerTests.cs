using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentTypesFilterCodeQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentTypesFilterCodeQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentTypesFilterCodeRequest();

        var query = new GetAppointmentTypesFilterCodeQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentTypesFilterCodeResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var request = new GetAppointmentTypesFilterCodeRequest
        {
            CodeFilterValue = code
        };

        var query = new GetAppointmentTypesFilterCodeQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentTypesFilterCodeResponse>(actual);

        Assert.NotNull(actual.AppointemntTypeCodes);

        Assert.NotEmpty(actual.AppointemntTypeCodes);

        Assert.StartsWith(code, actual.AppointemntTypeCodes.FirstOrDefault());
    }
}