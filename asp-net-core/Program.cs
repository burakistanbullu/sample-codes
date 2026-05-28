var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/", () => Results.Text("Hello from ASP.NET Core Demo Application\n"));

app.MapGet("/info", () => Results.Ok(new
{
    application = "asp-net-core-demo",
    framework = ".NET 10",
    environment = app.Environment.EnvironmentName,
    timestampUtc = DateTimeOffset.UtcNow
}));

app.MapHealthChecks("/healthz");
app.MapGet("/readyz", () => Results.Ok(new { status = "ready" }));

app.Run();
