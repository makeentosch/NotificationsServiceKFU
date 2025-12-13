var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume("rabbit-mq")
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("postgres-data")
    .WithLifetime(ContainerLifetime.Persistent);

var notificationsDb = postgres.AddDatabase("postgresNotifications");

var gateway = builder.AddProject<Projects.Gateway_Api>("gateway-api")
    .WithReference(notificationsDb)
    .WithReference(rabbitMq)
    .WaitFor(notificationsDb)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.MailService_Api>("mail-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.SmsService_Api>("sms-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.PushService_Api>("push-service")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.Build().Run();