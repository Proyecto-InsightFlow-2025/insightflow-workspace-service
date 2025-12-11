
using insightflow_workspace_service.src.Data;
using insightflow_workspace_service.src.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddScoped<WorkspaceHelper>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();


app.Run();
