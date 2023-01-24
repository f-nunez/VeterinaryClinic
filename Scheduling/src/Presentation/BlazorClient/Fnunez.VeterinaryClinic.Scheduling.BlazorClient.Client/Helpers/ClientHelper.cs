using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Clients;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class ClientHelper
{
    public static ClientDetailVm MapClientDetailViewModel(
        ClientDetailDto clientDetailDto)
    {
        return new ClientDetailVm
        {
            ClientId = clientDetailDto.ClientId,
            EmailAddress = clientDetailDto.EmailAddress,
            FullName = clientDetailDto.FullName,
            PreferredDoctorFullName = clientDetailDto.PreferredDoctorFullName,
            PreferredName = clientDetailDto.PreferredName,
            Salutation = clientDetailDto.Salutation
        };
    }

    public static ClientVm MapClientViewModel(ClientDto clientDto)
    {
        return new ClientVm
        {
            ClientId = clientDto.ClientId,
            EmailAddress = clientDto.EmailAddress,
            FullName = clientDto.FullName,
            PreferredDoctorId = clientDto.PreferredDoctorId,
            PreferredName = clientDto.PreferredName,
            Salutation = clientDto.Salutation
        };
    }
}