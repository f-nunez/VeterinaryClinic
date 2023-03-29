var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

app.AddWebApplicationBuilder();

app.Run();
