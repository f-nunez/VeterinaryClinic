using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Patients.Commands.CreatePatient;

[Collection(nameof(TestContextFixture))]
public class CreatePatientCommandHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public CreatePatientCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new CreatePatientRequest();

        var command = new CreatePatientCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsCreatePatientResponse()
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
        var patientPhotoData = new byte[] { 0x20 };
        var patientPhotoName = "test.jpeg";
        int? patientPreferredDoctorId = null;
        var patientSpecies = "c";

        var request = new CreatePatientRequest
        {
            Breed = patientBreed,
            ClientId = client.Id,
            Name = patientName,
            PhotoData = patientPhotoData,
            PhotoName = patientPhotoName,
            PreferredDoctorId = patientPreferredDoctorId,
            Sex = (int)patientAnimalSex,
            Species = patientSpecies
        };

        var command = new CreatePatientCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<CreatePatientResponse>(actual);
    }
}