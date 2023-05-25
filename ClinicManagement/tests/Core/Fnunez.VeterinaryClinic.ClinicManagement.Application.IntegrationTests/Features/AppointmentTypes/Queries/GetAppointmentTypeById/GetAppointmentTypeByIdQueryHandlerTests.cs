using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

[Collection(nameof(TestContextFixture))]
public class GetAppointmentTypeByIdQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetAppointmentTypeByIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetAppointmentTypeByIdRequest();

        var query = new GetAppointmentTypeByIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetAppointmentTypeByIdResponse()
    {
        // Arrange
        var code = "code";

        var duration = 60;

        var name = "name";

        var appointmentType = new AppointmentType(name, code, duration);

        await _fixture.AddAsync<AppointmentType>(appointmentType);

        var request = new GetAppointmentTypeByIdRequest
        {
            Id = appointmentType.Id
        };

        var query = new GetAppointmentTypeByIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetAppointmentTypeByIdResponse>(actual);

        Assert.NotNull(actual.AppointmentType);

        Assert.Equal(code, actual.AppointmentType.Code);

        Assert.Equal(duration, actual.AppointmentType.Duration);

        Assert.Equal(request.Id, actual.AppointmentType.Id);

        Assert.Equal(name, actual.AppointmentType.Name);
    }
}