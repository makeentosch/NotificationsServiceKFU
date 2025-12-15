using Hosting.NotificationServiceDefaults;
using SmsService.Application;
using SmsService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApplication();

builder.Services.AddSmsInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();