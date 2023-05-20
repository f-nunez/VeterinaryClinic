using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Patients.Commands.UpdatePatient;

[Collection(nameof(TestContextFixture))]
public class UpdatePatientCommandHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdatePatientCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdatePatientRequest();

        var command = new UpdatePatientCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdatePatientResponse()
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
        var patientBreed = "a";
        var patientName = "b";
        var patientAnimalType = new AnimalType("a", "a");
        var patientPhoto = new Photo("a", "a");
        var patientPhotoData = new byte[] { 0x20, 0x20 };
        var patientPhotoName = "test.jpeg";
        int? patientPreferredDoctorId = null;
        var patientSpecies = "c";

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

        patientAnimalSex = AnimalSex.Male;

        patientBreed = "aa";

        patientName = "bb";

        patientSpecies = "cc";

        var request = new UpdatePatientRequest
        {
            ClientId = client.Id,
            PatientId = patient.Id,
            Breed = patientBreed,
            IsNewPhoto = true,
            Name = patientName,
            PhotoData = patientPhotoData,
            PhotoName = patientPhotoName,
            PreferredDoctorId = patientPreferredDoctorId,
            Sex = (int)patientAnimalSex,
            Species = patientSpecies
        };

        var command = new UpdatePatientCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdatePatientResponse>(actual);
    }
}