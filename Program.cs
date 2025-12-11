using insightflow_workspace_service.src.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
builder.Services.AddSingleton<ApplicationDBContext>();

app.Run();
