using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Filters;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Hubs;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Services;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Settings;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationHub;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authenticationSetting = configuration
            .GetSection(typeof(AuthenticationSetting).Name)
            .Get<AuthenticationSetting>()!;

        var authorizationSetting = configuration
            .GetSection(typeof(AuthorizationSetting).Name)
            .Get<AuthorizationSetting>()!;

        var corsPolicySetting = configuration
            .GetSection(typeof(CorsPolicySetting).Name)
            .Get<CorsPolicySetting>()!;

        // ShowPII only for development stages
        IdentityModelEventSource.ShowPII = true;

        // Needed when run behind a reverse proxy
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto;
        });

        services.AddHttpContextAccessor();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(typeof(CorsPolicySetting).Name, corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyHeader();

                corsPolicyBuilder.AllowAnyMethod();

                corsPolicyBuilder.WithOrigins(
                    corsPolicySetting.BlazorServerUrl,
                    corsPolicySetting.IdentityServerUrl
                );
            });
        });

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddAuthentication(authenticationSetting.DefaultScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authenticationSetting.Authority;

                options.Audience = authenticationSetting.Audience;

                options.RequireHttpsMetadata = authenticationSetting.RequireHttpsMetadata;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = authenticationSetting.ValidateAudience
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/notificationhub")))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            foreach (var policy in authorizationSetting.Policies)
                options.AddPolicy(policy.Name, policyBuilder =>
                {
                    if (policy.RequireAuthenticatedUser)
                        policyBuilder.RequireAuthenticatedUser();

                    if (policy.RequiredClaims != null)
                        foreach (var requiredClaim in policy.RequiredClaims)
                            policyBuilder.RequireClaim(requiredClaim.ClaimType, requiredClaim.Values);

                    if (policy.RequiredRoles != null)
                        policyBuilder.RequireRole(policy.RequiredRoles);
                });
        });

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddSignalR();

        services.AddScoped<INotificationHubService, NotificationHubService>();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        switch (app.Environment.EnvironmentName)
        {
            case "DockerNginx":
                app.UseForwardedHeaders();
                app.UseSwagger();
                app.UseSwaggerUI();
                Task.Run(() => SeedDataAsync(app));
                break;
            case "DockerDevelopment":
            case "Development":
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHsts();
                app.UseHttpsRedirection();
                Task.Run(() => SeedDataAsync(app));
                break;
            default:
                app.UseHsts();
                app.UseHttpsRedirection();
                break;
        }

        app.UseRouting();

        app.UseCors(typeof(CorsPolicySetting).Name);

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseHealthChecks("/api/health");

        app.MapControllers().RequireAuthorization("ApiScope");

        app.MapHub<NotificationHub>("/notificationHub");

        return app;
    }

    private static async void SeedDataAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContextSeeder>();

            await seeder.MigrateAsync();

            await seeder.SeedDataAsync();
        }
    }
}