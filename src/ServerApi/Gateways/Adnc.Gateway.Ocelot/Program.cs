using Adnc.Infra.Consul.Configuration;

namespace Adnc.Gateway.Ocelot;

internal class Program
{
    internal static async Task Main(string[] args) =>
    await CreateHostBuilder(args)
                        .Build()
                        .ChangeThreadPoolSettings()
                        .RunAsync();

    internal static IHostBuilder CreateHostBuilder(string[] args) =>
    Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
        .ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            if (env.IsProduction() || env.IsStaging())
            {
                var configuration = config.Build();
                var consulOption = configuration.GetSection("Consul").Get<ConsulOptions>();
                config.AddConsulConfiguration(consulOption, true);
            }
            else
            {
                config.AddJsonFile($"{AppContext.BaseDirectory}/Config/ocelot.consul.json", true, true);
            }
        });
}
