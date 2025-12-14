using Hosting.NotificationServiceDefaults;
using PushService.Application;
using PushService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApplication();

builder.Services.AddPushInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();