using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Mappings;

public class DoctorProfileTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DoctorProfile>();
        });

        // Act
        var mapper = new Mapper(mapperConfiguration);

        //Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}