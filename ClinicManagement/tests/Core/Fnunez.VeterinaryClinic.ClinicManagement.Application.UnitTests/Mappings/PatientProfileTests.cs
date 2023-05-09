using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Mappings;

public class PatientProfileTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PatientProfile>();
        });

        // Act
        var mapper = new Mapper(mapperConfiguration);

        //Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}