
//var webApiAssembly = System.Reflection.Assembly.GetExecutingAssembly();

using Adnc.Shared.WebApi;
using Microsoft.Extensions.Hosting;

var startAssembly = System.Reflection.Assembly.GetExecutingAssembly();
var startAssemblyName = startAssembly.GetName().Name ?? string.Empty;
var lastName = startAssemblyName.Split('.').Last();
var serviceInfo = ServiceInfo.CreateInstance(startAssembly, migrationsAssemblyName: startAssemblyName);
var builder = WebApplication
    .CreateBuilder(args);
var config = builder
    .ConfigureAdncDefault(serviceInfo);
var app = config.Build();



app.UseAdnc();
// Add services to the container.

app.ChangeThreadPoolSettings()
    .UseRegistrationCenter();
app.MapGet("/", async context =>
{
    var content = serviceInfo.GetDefaultPageContent(app.Services);
    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync(content);
});
// Configure the HTTP request pipeline.

await app.RunAsync();
