
using insightflow_workspace_service.src;
using insightflow_workspace_service.src.Data;
using insightflow_workspace_service.src.Helpers;
using insightflow_workspace_service.src.Service;
using Microsoft.Extensions.Options;

Environment.SetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "true");


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton<ApplicationDBContext>();

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    var account = new CloudinaryDotNet.Account(
        config.CloudName,
        config.ApiKey,
        config.ApiSecret
    );

    return new CloudinaryDotNet.Cloudinary(account);
});

builder.Services.AddScoped<WorkspaceHelper>();
builder.Services.AddScoped<CloudinaryService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();


app.Run();
