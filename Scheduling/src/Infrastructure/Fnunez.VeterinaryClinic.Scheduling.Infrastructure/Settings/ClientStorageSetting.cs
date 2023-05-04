using Fnunez.VeterinaryClinic.Scheduling.Application.Settings;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Settings;

public class ClientStorageSetting : IClientStorageSetting
{
    public string BasePath { get; set; } = null!;
}