using Adnc.Infra.Consul.Registrar;

namespace Microsoft.Extensions.Hosting;

public static class ApplicationBuilderConsulExtension
{
    public static IHost RegisterToConsul(this IHost host, string? serviceId = null)
    {
        var kestrelConfig = host.Services.GetRequiredService<IOptions<KestrelOptions>>().Value;
        if (kestrelConfig is null)
            throw new NotImplementedException(nameof(kestrelConfig));

        var registration = ActivatorUtilities.CreateInstance<RegistrationProvider>(host.Services);
        var ipAddresses = registration.GetLocalIpAddress("InterNetwork");
        if (ipAddresses.IsNullOrEmpty())
            throw new NotImplementedException(nameof(kestrelConfig));

        var defaultEnpoint = kestrelConfig.Endpoints.FirstOrDefault(x => x.Key.EqualsIgnoreCase("default")).Value;
        //var grpcEnpoint = kestrelConfig.Endpoints.FirstOrDefault(x => x.Key.EqualsIgnoreCase("grpc")).Value;
        if (defaultEnpoint is null || defaultEnpoint.Url.IsNullOrWhiteSpace())
            throw new NotImplementedException(nameof(kestrelConfig));
        //环境变量参数获取
        var ip = Environment.GetEnvironmentVariable("IP");
        string? portEnv = Environment.GetEnvironmentVariable("PORT");
        //string? grpcPortEnv = Environment.GetEnvironmentVariable("GRPC_PORT");
        if (!portEnv.IsNullOrEmpty())
            defaultEnpoint.Url = defaultEnpoint.Url.Split(':')[0] + ":" + defaultEnpoint.Url.Split(':')[1] + ":" + portEnv;
        /*if (!grpcPortEnv.IsNullOrEmpty())
            grpcEnpoint.Url = grpcEnpoint.Url.Split(":")[0] + ":" + grpcEnpoint.Url.Split(":")[1] + ":" + grpcPortEnv;*/
        var serviceAddress = new Uri(defaultEnpoint.Url);
        if (string.IsNullOrWhiteSpace(ip))
            ip = ipAddresses.FirstOrDefault();
        if (serviceAddress.Host == "0.0.0.0")
            serviceAddress = new Uri($"{serviceAddress.Scheme}://{ip}:{serviceAddress.Port}");

        registration.Register(serviceAddress, serviceId);
        return host;
    }

    public static IHost RegisterToConsul(this IHost host, Uri serviceAddress, string? serviceId = null)
    {
        if (serviceAddress is null)
            throw new ArgumentNullException(nameof(serviceAddress));

        var registration = ActivatorUtilities.CreateInstance<RegistrationProvider>(host.Services);
        registration.Register(serviceAddress, serviceId);
        return host;
    }

    public static IHost RegisterToConsul(this IHost host, AgentServiceRegistration instance)
    {
        if (instance is null)
            throw new ArgumentNullException(nameof(instance));

        var registration = ActivatorUtilities.CreateInstance<RegistrationProvider>(host.Services);
        registration.Register(instance);
        return host;
    }
}