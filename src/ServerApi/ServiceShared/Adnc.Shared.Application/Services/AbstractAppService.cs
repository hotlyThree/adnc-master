using Adnc.Infra.Core.Guard;
using AspectCore.Extensions.Reflection;

namespace Adnc.Shared.Application.Services;

public abstract class AbstractAppService : IAppService
{
    public IObjectMapper Mapper
    {
        get
        {
            //var httpContext = InfraHelper.Accessor.GetCurrentHttpContext();
            //if (httpContext is not null)
            //    return httpContext.RequestServices.GetRequiredService<IObjectMapper>();
            if (ServiceLocator.Provider is not null)
            {
                var mapper = ServiceLocator.Provider.GetService<IObjectMapper>();
                return mapper ?? throw new NullReferenceException("'mapper = ServiceLocator.Provider.GetService' is null");
            }
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 自定义映射，解决默认值不映射
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDest"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    protected void ConditionalMap<TSource, TDest>(TSource source, TDest destination)
    where TSource : class
    where TDest : class
    {
        var sourceType = typeof(TSource);
        var destType = typeof(TDest);

        foreach (var prop in sourceType.GetProperties())
        {
            var destProp = destType.GetProperty(prop.Name);
            if (destProp != null)
            {
                var defaultValue = prop.PropertyType.GetDefaultValue();
                var sourceValue = prop.GetValue(source);
                if (sourceValue != null && !sourceValue.Equals(defaultValue))
                {
                    destProp.SetValue(destination, sourceValue);
                }
            }
        }
    }

    protected AppSrvResult AppSrvResult() => new();

    protected AppSrvResult<TValue> AppSrvResult<TValue>(TValue value)
    {
        Checker.Argument.IsNotNull(value, nameof(value));
        return new AppSrvResult<TValue>(value);
    }

    protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string? detail = null, string? title = null, string? instance = null, string? type = null) => new(statusCode, detail, title, instance, type);

    protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions) => expressions;
}