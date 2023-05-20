using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Mappings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.UnitTests.Services.EmailEngine.Mappings;

public class AppointmentProfileTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AppointmentProfile>();
        });

        // Act
        var mapper = new Mapper(mapperConfiguration);

        //Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}