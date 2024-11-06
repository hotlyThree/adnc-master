using Adnc.Fstorch.File.Application.FileServicesExtensions.Options;
using Adnc.Fstorch.File.Application.FileTask;
using Microsoft.Extensions.Hosting;

namespace Adnc.Fstorch.File.Application.FileServicesExtensions
{
    public static class FileCleanupServiceExtensions
    {
        /// <summary>
        /// 注册文件清理
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileCleanupService(this IServiceCollection services)
        {
            var options = new FileCleanupServiceOptions();
            //可以设置从配置文件获取参数
            //Action<FileCleanupServiceOptions> setupAction
            //setupAction(options);

            services.AddSingleton<IHostedService>(provider => new CleanTempFileTask());

            return services;
        }
    }
}
