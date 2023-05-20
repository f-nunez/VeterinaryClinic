using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Doctors.Queries.GetDoctorsFilterId;

[Collection(nameof(TestContextFixture))]
public class GetDoctorsFilterIdQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetDoctorsFilterIdQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetDoctorsFilterIdRequest();

        var query = new GetDoctorsFilterIdQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetDoctorsFilterIdResponse()
    {
        // Arrange
        var fullName = "a";

        var doctor = new Doctor(fullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var request = new GetDoctorsFilterIdRequest
        {
            IdFilterValue = doctor.Id.ToString()
        };

        var query = new GetDoctorsFilterIdQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetDoctorsFilterIdResponse>(actual);

        Assert.NotNull(actual.DoctorIds);

        Assert.NotEmpty(actual.DoctorIds);

        Assert.StartsWith(doctor.Id.ToString(), actual.DoctorIds.FirstOrDefault());
    }
}