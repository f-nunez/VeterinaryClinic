using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Settings;
using Microsoft.AspNetCore.Components.Authorization;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.BackendForFrontend;

public class BffAuthenticationStateProvider : AuthenticationStateProvider
{
    private static int MilliSecondsPerSecond = 1000;
    private readonly IBackendForFrontendSetting _bffSetting;
    private ClaimsPrincipal _cachedUser;
    private readonly HttpClient _client;
    private readonly ILogger<BffAuthenticationStateProvider> _logger;
    private readonly TimeSpan _userCacheRefreshInterval;
    private DateTimeOffset _userLastCheck;

    public BffAuthenticationStateProvider(
        IBackendForFrontendSetting bffSetting,
        HttpClient httpClient,
        ILogger<BffAuthenticationStateProvider> logger)
    {
        _bffSetting = bffSetting;
        _cachedUser = new(new ClaimsIdentity());
        _client = httpClient;
        _logger = logger;

        _userCacheRefreshInterval = TimeSpan
            .FromSeconds(_bffSetting.SecondsToRefreshUserCache);

        _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await GetUser();
        var state = new AuthenticationState(user);

        // checks periodically for a session state change and fires event
        // this causes a round trip to the server
        // adjust the period accordingly if that feature is needed
        if (user.Identity!.IsAuthenticated!)
        {
            _logger.LogInformation("starting background check..");

            Timer? timer = null;

            timer = new Timer(async _ =>
            {
                var currentUser = await GetUser(false);
                if (currentUser.Identity!.IsAuthenticated == false)
                {
                    _logger.LogInformation("user logged out");

                    NotifyAuthenticationStateChanged(
                        Task.FromResult(new AuthenticationState(currentUser)));

                    await timer!.DisposeAsync();
                }
            },
            null,
            _bffSetting.SecondsToCheckAuthenticationStateDuetime * MilliSecondsPerSecond,
            _bffSetting.SecondsToCheckAuthenticationStatePeriod * MilliSecondsPerSecond);
        }

        return state;
    }

    private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = true)
    {
        var now = DateTimeOffset.Now;
        if (useCache && now < _userLastCheck + _userCacheRefreshInterval)
        {
            _logger.LogDebug("Taking user from cache");
            return _cachedUser;
        }

        _logger.LogDebug("Fetching user");
        _cachedUser = await FetchUser();
        _userLastCheck = now;

        return _cachedUser;
    }

    record ClaimRecord(string Type, object Value);

    private async Task<ClaimsPrincipal> FetchUser()
    {
        try
        {
            _logger.LogInformation("Fetching user information.");
            var response = await _client.GetAsync("bff/user?slide=false");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var claims = await response.Content
                    .ReadFromJsonAsync<List<ClaimRecord>>();

                var identity = new ClaimsIdentity(
                    nameof(BffAuthenticationStateProvider),
                    "name",
                    "role");

                foreach (var claim in claims!)
                    identity.AddClaim(
                        new Claim(claim.Type, claim.Value.ToString()!));

                return new ClaimsPrincipal(identity);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Fetching user failed.");
        }

        return new ClaimsPrincipal(new ClaimsIdentity());
    }
}