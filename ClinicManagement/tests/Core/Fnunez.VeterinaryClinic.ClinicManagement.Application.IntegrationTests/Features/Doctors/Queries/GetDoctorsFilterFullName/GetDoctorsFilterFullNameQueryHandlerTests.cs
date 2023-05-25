using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Doctors.Queries.GetDoctorsFilterFullName;

[Collection(nameof(TestContextFixture))]
public class GetDoctorsFilterFullNameQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetDoctorsFilterFullNameQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetDoctorsFilterFullNameRequest();

        var query = new GetDoctorsFilterFullNameQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetDoctorsFilterFullNameResponse()
    {
        // Arrange
        var fullName = "a";

        var doctor = new Doctor(fullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var request = new GetDoctorsFilterFullNameRequest
        {
            FullNameFilterValue = fullName
        };

        var query = new GetDoctorsFilterFullNameQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetDoctorsFilterFullNameResponse>(actual);

        Assert.NotNull(actual.DoctorFullNames);

        Assert.NotEmpty(actual.DoctorFullNames);

        Assert.StartsWith(fullName, actual.DoctorFullNames.FirstOrDefault());
    }
}