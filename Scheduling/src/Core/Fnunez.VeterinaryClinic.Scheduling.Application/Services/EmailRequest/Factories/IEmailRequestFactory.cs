using Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Requests;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.EmailRequest.Factories;

public interface IEmailRequestFactory
{
    BaseEmailRequest CreateEmailRequest();
    string GetEmailEvent();
}