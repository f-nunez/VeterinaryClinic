using Fnunez.VeterinaryClinic.Public.Web.Services.Appointment;
using Fnunez.VeterinaryClinic.Public.Web.ViewModels.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Public.Web.Controllers;

public class AppointmentController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(
        IAppointmentService appointmentService,
        ILogger<AppointmentController> logger)
    {
        _appointmentService = appointmentService;
        _logger = logger; ;
    }

    [HttpGet]
    public async Task<ActionResult> Confirm(string id, string language)
    {
        try
        {
            await _appointmentService.ConfirmAppointmentAsync(id);

            var viewModel = new ConfirmVm
            {
                Language = language
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            var viewModel = new ConfirmErrorVm
            {
                Language = language
            };

            return View("ConfirmError.cshtml", viewModel);
        }
    }
}