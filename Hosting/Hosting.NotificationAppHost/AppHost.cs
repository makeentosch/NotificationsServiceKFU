var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume("rabbit-mq")
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("postgres-data")
    .WithArgs("-c", "max_connections=300")
    .WithLifetime(ContainerLifetime.Persistent);

var notificationsDb = postgres.AddDatabase("postgresNotifications");

builder.AddProject<Projects.Gateway_Api>("gateway-api")
    .WithReference(notificationsDb)
    .WithReference(rabbitMq)
    .WaitFor(notificationsDb)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.MailService_Api>("mail-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("MassTransit__RetryLimit", "5")
    .WithEnvironment("MassTransit__MinIntervalMilliseconds", "1000");

builder.AddProject<Projects.SmsService_Api>("sms-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("MassTransit__RetryLimit", "1") 
    .WithEnvironment("MassTransit__MinIntervalMilliseconds", "100");

builder.AddProject<Projects.PushService_Api>("push-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.Build().Run();