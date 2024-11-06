using Adnc.Fstorch.User.Application.WeChatTask;

namespace Adnc.Fstorch.User.Application.Extensions
{
    public static class OfficeAccountExtension
    {
        public static IServiceCollection AddOfficeAccountSubScribe(this IServiceCollection services)
        {
            return services.AddHostedService<AddOfficeAccountSubScribeTask>();
        }
    }
}
