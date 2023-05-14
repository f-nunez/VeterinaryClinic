using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

[Collection(nameof(TestContextFixture))]
public class GetPatientsFilterPreferredDoctorQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetPatientsFilterPreferredDoctorQueryHandlerTests(
        TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetPatientsFilterPreferredDoctorRequest();

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetPatientsFilterPreferredDoctorResponse()
    {
        // Arrange
        var doctorFullName = "doc";

        var doctor = new Doctor(doctorFullName);

        await _fixture.AddAsync<Doctor>(doctor);

        var take = 1;

        var request = new GetPatientsFilterPreferredDoctorRequest
        {
            DataGridRequest = new DataGridRequest
            {
                Search = doctorFullName,
                Take = take
            }
        };

        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetPatientsFilterPreferredDoctorResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.StartsWith(doctorFullName, actual.DataGridResponse.Items.FirstOrDefault()?.FullName);
    }
}