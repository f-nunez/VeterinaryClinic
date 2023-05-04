using System.Security.Claims;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);
}