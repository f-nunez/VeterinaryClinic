using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Public.Web.Controllers;

public class AppointmentController : Controller
{
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(ILogger<AppointmentController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Confirm(Guid id)
    {
        return View();
    }
}