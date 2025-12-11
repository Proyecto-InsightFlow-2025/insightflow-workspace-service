using insightflow_workspace_service.src.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ApplicationDBContext>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
