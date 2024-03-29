using Duende.Bff.Yarp;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddBlazorServerWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ShowPII only for development stages
        IdentityModelEventSource.ShowPII = true;

        // Needed when run behind a reverse proxy
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.AddControllersWithViews();

        services.AddRazorPages();

        services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders;
        });

        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxyForNotificationHubSignalr"))
            .AddTransforms<AccessTokenTransformProvider>()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        services.AddBff().AddRemoteApis();

        var authenticationSetting = configuration
            .GetSection(typeof(AuthenticationSetting).Name)
            .Get<AuthenticationSetting>()!;

        var cookieSetting = configuration
            .GetSection(typeof(CookieSetting).Name)
            .Get<CookieSetting>()!;

        var corsPolicySetting = configuration
            .GetSection(typeof(CorsPolicySetting).Name)
            .Get<CorsPolicySetting>()!;

        var openIdConnectSetting = configuration
            .GetSection(typeof(OpenIdConnectSetting).Name)
            .Get<OpenIdConnectSetting>()!;

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = authenticationSetting.DefaultScheme;
            options.DefaultChallengeScheme = authenticationSetting.DefaultChallengeScheme;
            options.DefaultSignOutScheme = authenticationSetting.DefaultSignOutScheme;
        })
        .AddCookie(cookieSetting.AuthenticationScheme, options =>
        {
            options.Cookie.Name = cookieSetting.Name;
            options.Cookie.SameSite = cookieSetting.SameSiteMode;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieSetting.ExpireTimeInMinutes);
            options.SlidingExpiration = cookieSetting.EnabledSlidingExpiration;
        })
        .AddOpenIdConnect(openIdConnectSetting.AuthenticationScheme, options =>
        {
            options.AccessDeniedPath = openIdConnectSetting.AccessDeniedPath;
            options.Authority = openIdConnectSetting.Authority;
            options.ClientId = openIdConnectSetting.ClientId;
            options.UsePkce = openIdConnectSetting.EnabledUsePkce;
            options.ClientSecret = openIdConnectSetting.ClientSecret;
            options.ResponseType = openIdConnectSetting.ResponseType;
            options.ResponseMode = openIdConnectSetting.ResponseMode;

            options.RequireHttpsMetadata = openIdConnectSetting.EnabledRequireHttpsMetadata;

            if (string.IsNullOrEmpty(openIdConnectSetting.MetadataAddress))
                options.MetadataAddress = openIdConnectSetting.MetadataAddress;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = openIdConnectSetting.EnabledValidateAudience
            };

            options.Scope.Clear();

            foreach (var scope in openIdConnectSetting.Scopes)
                options.Scope.Add(scope);

            foreach (var claimAction in openIdConnectSetting.ClaimActionsToMap)
                options.ClaimActions.MapUniqueJsonKey(claimAction, claimAction);

            options.MapInboundClaims = openIdConnectSetting.EnabledMapInboundClaims;
            options.GetClaimsFromUserInfoEndpoint = openIdConnectSetting.EnabledGetClaimsFromUserInfoEndpoint;
            options.SaveTokens = openIdConnectSetting.EnabledSaveTokens;
        });

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(typeof(CorsPolicySetting).Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();

                corsPolicyBuilder.AllowAnyMethod();

                corsPolicyBuilder.WithOrigins(
                    corsPolicySetting.IdentityServerUrl,
                    corsPolicySetting.SchedulingApiUrl,
                    corsPolicySetting.SchedulingNotificationsApiUrl
                );
            });
        });

        return services;
    }

    public static WebApplication AddBlazorServerWebApplicationBuilder(
        this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        SetMiddlewaresAccordingToEnvironment(app);

        app.UseHttpLogging();

        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors(typeof(CorsPolicySetting).Name);

        app.UseAuthentication();

        app.UseBff();

        app.UseAuthorization();

        app.MapBffManagementEndpoints();

        app.MapRazorPages();

        app.MapReverseProxy();

        app.MapControllers();

        app.MapFallbackToFile("index.html");

        return app;
    }

    private static void SetMiddlewaresAccordingToEnvironment(WebApplication app)
    {
        string environmentName = string.Empty;

        var deploymentSetting = app.Configuration
            .GetSection(typeof(DeploymentSetting).Name)
            .Get<DeploymentSetting>();

        if (deploymentSetting is null ||
            string.IsNullOrEmpty(deploymentSetting.EnvironmentName))
            environmentName = app.Environment.EnvironmentName;
        else
            environmentName = deploymentSetting.EnvironmentName;

        switch (environmentName)
        {
            case "DockerNginx":
                app.AddHttpsRequestScheme();
                app.UseForwardedHeaders();
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.AddWellKnownHttRedirection(deploymentSetting);
                break;
            case "DockerDevelopment":
            case "Development":
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseHsts();
                app.UseHttpsRedirection();
                break;
            default:
                app.UseHsts();
                app.UseHttpsRedirection();
                break;
        }
    }

    private static void AddHttpsRequestScheme(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            context.Request.Scheme = "https";

            await next(context);
        });
    }

    private static void AddWellKnownHttRedirection(
        this WebApplication app,
        DeploymentSetting? deploymentSetting)
    {
        if (deploymentSetting is null ||
            string.IsNullOrEmpty(deploymentSetting.WellKnownHttpHeaderToReplace) ||
            string.IsNullOrEmpty(deploymentSetting.WellKnownHttpHeaderReplacement))
            return;

        app.Use(async (context, next) =>
        {
            await next(context);

            if (context.Response.StatusCode == StatusCodes.Status302Found)
            {
                string? location = context.Response.Headers[HeaderNames.Location];

                if (!string.IsNullOrEmpty(location) &&
                    location.Contains(deploymentSetting.WellKnownHttpHeaderToReplace))
                {
                    location = location.Replace(
                        deploymentSetting.WellKnownHttpHeaderToReplace,
                        deploymentSetting.WellKnownHttpHeaderReplacement
                    );

                    context.Response.Headers[HeaderNames.Location] = location;
                }
            }
        });
    }
}