
using insightflow_workspace_service.src.Data;
using insightflow_workspace_service.src.Helpers;

Environment.SetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "true");


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddScoped<WorkspaceHelper>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();


app.Run();
