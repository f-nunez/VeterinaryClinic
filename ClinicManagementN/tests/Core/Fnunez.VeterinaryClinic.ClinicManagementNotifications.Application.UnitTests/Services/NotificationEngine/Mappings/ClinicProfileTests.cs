using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.UnitTests.Services.NotificationEngine.Mappings;

public class ClinicProfileTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClinicProfile>();
        });

        // Act
        var mapper = new Mapper(mapperConfiguration);

        //Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}