using Adnc.Shared.WebApi.Authentication;
using Adnc.Shared.WebApi.Authorization;

namespace Adnc.Shared.WebApi.Registrar;

public abstract partial class AbstractWebApiDependencyRegistrar : IDependencyRegistrar
{
    public string Name => "webapi";
    protected IConfiguration Configuration { get; init; } = default!;
    protected IServiceCollection Services { get; init; } = default!;
    protected IServiceInfo ServiceInfo { get; init; } = default!;

    /// <summary>
    /// 服务注册与系统配置
    /// </summary>
    /// <param name="services"><see cref="IServiceInfo"/></param>
    protected AbstractWebApiDependencyRegistrar(IServiceCollection services)
    {
        Services = services;
        Configuration = services.GetConfiguration();
        ServiceInfo = services.GetServiceInfo();
    }

    /// <summary>
    /// 注册服务入口方法
    /// </summary>
    public abstract void AddAdnc();

    /// <summary>
    /// 注册Webapi通用的服务
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    protected virtual void AddWebApiDefault() =>
        AddWebApiDefault<BearerAuthenticationRemoteProcessor, PermissionRemoteHandler>();
    protected virtual void AddWebApiNoAuthDefault() =>
        AddWebApiNoAuthDefault("non");


    protected virtual void AddWebApiNoAuthDefault(string ctl)
    {
        Services
            .AddHttpContextAccessor()
            .AddMemoryCache();

        Configure();
        AddControllers();
        AddCors();

        var enableSwaggerUI = Configuration.GetValue(NodeConsts.SwaggerUI_Enable, true);
        if (enableSwaggerUI)
        {
            AddSwaggerGen();
            AddMiniProfiler();
        }

        AddApplicationServices();
    }

    /// <summary>
    /// 注册Webapi通用的服务
    /// </summary>
    /// <typeparam name="TAuthenticationProcessor"><see cref="AbstractAuthenticationProcessor"/></typeparam>
    /// <typeparam name="TAuthorizationHandler"><see cref="AbstractPermissionHandler"/></typeparam>
    protected virtual void AddWebApiDefault<TAuthenticationProcessor, TAuthorizationHandler>()
        where TAuthenticationProcessor : AbstractAuthenticationProcessor
        where TAuthorizationHandler : AbstractPermissionHandler
    {
        Services
            .AddHttpContextAccessor()
            .AddMemoryCache();

        Configure();
        AddControllers();
        AddAuthentication<TAuthenticationProcessor>();
        AddAuthorization<TAuthorizationHandler>();
        AddCors();

        var enableSwaggerUI = Configuration.GetValue(NodeConsts.SwaggerUI_Enable, true);
        if(enableSwaggerUI)
        {
            AddSwaggerGen();
            AddMiniProfiler();
        }

        AddApplicationServices();
    }
}