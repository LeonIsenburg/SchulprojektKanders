using Aspire.Hosting.Docker.Resources.ServiceNodes;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose")
    .ConfigureComposeFile(file => file.AddVolume(new Volume { Name = "appdb-data" }));

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithUrlForEndpoint("https", _ => new() { Url = "/swagger", DisplayText = "Swagger" });

if (builder.ExecutionContext.IsRunMode)
{
    var dbDirectory = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "..", "Backend"));
    var db = builder.AddSqlite("appdb", dbDirectory, "app.db");

    backend.WithReference(db);
}
else
{
    backend
        .WithEnvironment("ConnectionStrings__appdb", "Data Source=/home/app/app.db")
        .PublishAsDockerComposeService((_, service) =>
        {
            service.AddVolume(new Volume { Name = "appdb-data", Source = "appdb-data", Target = "/home/app", Type = "volume" });
            service.Ports = ["8081:${BACKEND_PORT}"];
        });
}

#pragma warning disable ASPIREJAVASCRIPT001
builder.AddViteApp("frontend", "../Frontend")
    .WithReference(backend)
    .WithEnvironment("BACKEND_URL", backend.GetEndpoint("https"))
    .WaitFor(backend)
    .WithExternalHttpEndpoints()
    .PublishAsStaticWebsite("/api", backend)
    .PublishAsDockerComposeService((_, service) => service.Ports = ["8080:5000"]);
#pragma warning restore ASPIREJAVASCRIPT001

builder.Build().Run();
