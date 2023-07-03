using System.Reflection;
using FastFood.Application;
using FastFood.Application.Behaviors;
using FastFood.Infrastructure;
using FastFood.Infrastructure.Options;
using FastFood.Presentation.Hubs;
using FastFood.Presentation.Middleware;
using FastFood.Presentation.Services;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<INotificationService, HubNotificationService>();

builder.Host.UseSerilog((_, _, configuration) => configuration
    .WriteTo.Console()
    .WriteTo.File("log.txt"));

var app = builder.Build();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Share image folder on web
var imageOptions = app.Services.GetService<IOptions<ImageOptions>>()?.Value;

if (imageOptions is not null)
{
    var imageFolder = imageOptions.Folder;
    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
    var imageDir = Path.Combine(baseDir, imageFolder);

    if (!Directory.Exists(imageDir))
        Directory.CreateDirectory(imageDir);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(imageDir),
        RequestPath = $"/{imageFolder}"
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options => options.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<MessageHub>("/hub/message");

app.MapDefaultControllerRoute();

app.MapFallbackToFile("index.html");

app.Run();