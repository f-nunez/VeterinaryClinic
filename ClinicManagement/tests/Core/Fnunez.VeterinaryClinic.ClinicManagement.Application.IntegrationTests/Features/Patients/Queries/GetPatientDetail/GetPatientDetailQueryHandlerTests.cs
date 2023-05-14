using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Patients.Queries.GetPatientDetail;

[Collection(nameof(TestContextFixture))]
public class GetPatientDetailQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetPatientDetailQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetPatientDetailRequest();

        var query = new GetPatientDetailQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetPatientDetailResponse()
    {
        // Arrange
        var clientEmailAddress = "test@nunez.ninja";

        var clientFullName = "a";

        int? clientPreferredDoctorId = null;

        var clientPreferredLanguage = PreferredLanguage.English;

        var clientPreferredName = "b";

        var clientSalutation = "Mister a";

        var client = new Client
        (
            clientFullName,
            clientPreferredName,
            clientSalutation,
            clientEmailAddress,
            clientPreferredLanguage,
            clientPreferredDoctorId
        );

        await _fixture.AddAsync<Client>(client);

        var patientAnimalSex = AnimalSex.Female;
        var patientAnimalType = new AnimalType("a", "a");
        var patientName = "a";
        var patientPhoto = new Photo("a", "a");
        int? patientPreferredDoctorId = null;

        var patient = new Patient(
            client.Id,
            patientName,
            patientAnimalSex,
            patientAnimalType,
            patientPhoto,
            patientPreferredDoctorId
        );

        client.AddPatient(patient);

        await _fixture.UpdateAsync<Client>(client);

        var request = new GetPatientDetailRequest
        {
            ClientId = client.Id,
            PatientId = patient.Id
        };

        var query = new GetPatientDetailQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetPatientDetailResponse>(actual);

        Assert.NotNull(actual.PatientDetail);

        Assert.Equal(patientAnimalType.Breed, actual.PatientDetail.Breed);

        Assert.True(actual.PatientDetail.IsActive);

        Assert.Equal(patientName, actual.PatientDetail.Name);

        Assert.Equal(patientPhoto.Name, actual.PatientDetail.PhotoName);

        Assert.Empty(actual.PatientDetail.PreferredDoctorFullName);

        Assert.Equal((int)patientAnimalSex, actual.PatientDetail.Sex);

        Assert.Equal(patientAnimalType.Species, actual.PatientDetail.Species);
    }
}