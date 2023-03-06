using Duende.Bff.Yarp;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Server.Settings;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddBlazorServerWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddRazorPages();

        services.AddReverseProxy()
            .AddTransforms<AccessTokenTransformProvider>()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        services.AddBff();

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
                    corsPolicySetting.ClinicManagementApiUrl,
                    corsPolicySetting.ClinicManagementNotificationsApiUrl,
                    corsPolicySetting.IdentityServerUrl
                );
            });
        });

        return services;
    }

    public static WebApplication AddBlazorServerWebApplicationBuilder(
        this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseRouting();

        app.UseCors(typeof(CorsPolicySetting).Name);

        app.UseBff();

        app.UseAuthorization();

        app.MapBffManagementEndpoints();

        app.MapRazorPages();

        app.MapBffReverseProxy();

        app.MapFallbackToFile("index.html");

        return app;
    }
}