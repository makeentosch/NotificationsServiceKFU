using Gateway.Application;
using Gateway.Infrastructure;
using Hosting.NotificationServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); 

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Gateway.Infrastructure.DataStorage.GatewayDbContext>();
    context.Database.EnsureCreated();
}

app.Run();