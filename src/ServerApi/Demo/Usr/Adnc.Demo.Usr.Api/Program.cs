using NLog;
using NLog.Web;
using ProtoBuf.Meta;

namespace Adnc.Demo.Usr.Api;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug($"init {nameof(Program.Main)}");
        try
        {
            var startAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var startAssemblyName = startAssembly.GetName().Name ?? string.Empty;
            var lastName = startAssemblyName.Split('.').Last();
            var migrationsAssemblyName = startAssemblyName.Replace($".{lastName}", ".Repository");
            var serviceInfo = ServiceInfo.CreateInstance(startAssembly, migrationsAssemblyName);

            //Configuration,ServiceCollection,Logging,WebHost(Kestrel)
            var app = WebApplication
                .CreateBuilder(args)
                .ConfigureAdncDefault(serviceInfo)
                .Build();
            //Middlewares
            app.UseAdnc();
            //app.UseAuthentication();
            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", "Adnc.Demo.Usr.Api V1");
                c.RoutePrefix = string.Empty;
            });*/
            //Start
            app.ChangeThreadPoolSettings()
                .UseRegistrationCenter();

            //Default page
            app.MapGet("/", async context =>
            {
                var content = serviceInfo.GetDefaultPageContent(app.Services);
                context.Response.Headers.Add("Content-Type", "text/html");
                await context.Response.WriteAsync(content);
            });

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }
}