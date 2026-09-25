using Aspire.Hosting.Docker.Resources.ServiceNodes;

var builder = DistributedApplication.CreateBuilder(args);

// Ziel für "aspire publish": erzeugt eine docker-compose.yaml
builder.AddDockerComposeEnvironment("compose")
    .ConfigureComposeFile(file => file.AddVolume(new Volume { Name = "appdb-data" }));

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

if (builder.ExecutionContext.IsRunMode)
{
    // Lokal: die bestehende Backend/app.db weiterverwenden
    var dbDirectory = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "..", "Backend"));
    var db = builder.AddSqlite("appdb", dbDirectory, "app.db");

    backend.WithReference(db);
}
else
{
    // Docker: die DB-Datei liegt in einem Named Volume, damit die Daten Neustarts überleben.
    // Das Image läuft als Nicht-Root-User "app"; /home/app gehört diesem User, und Docker
    // übernimmt den Besitzer beim ersten Anlegen des Volumes -> SQLite darf dort schreiben.
    backend
        .WithEnvironment("ConnectionStrings__appdb", "Data Source=/home/app/app.db")
        .PublishAsDockerComposeService((_, service) =>
        {
            service.AddVolume(new Volume { Name = "appdb-data", Source = "appdb-data", Target = "/home/app", Type = "volume" });
            service.Ports = ["8081:${BACKEND_PORT}"];
        });
}

#pragma warning disable ASPIREJAVASCRIPT001 // PublishAsStaticWebsite ist in Aspire 13.5 noch experimentell
builder.AddViteApp("frontend", "../Frontend")
    .WithReference(backend)
    .WithEnvironment("BACKEND_URL", backend.GetEndpoint("http"))
    .WaitFor(backend)
    .WithExternalHttpEndpoints()
    // Docker: gebaute Dateien per YARP ausliefern und /api an das Backend weiterleiten
    .PublishAsStaticWebsite("/api", backend)
    .PublishAsDockerComposeService((_, service) => service.Ports = ["8080:5000"]);
#pragma warning restore ASPIREJAVASCRIPT001

builder.Build().Run();
