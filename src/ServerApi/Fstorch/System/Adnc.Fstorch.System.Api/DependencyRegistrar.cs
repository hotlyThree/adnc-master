using Adnc.Fstorch.System.Api.Authentication;
using Adnc.Fstorch.System.Api.Authorization;
using Adnc.Shared.WebApi.Registrar;
using System.Reflection;

namespace Adnc.Fstorch.System.Api
{
    public sealed class UserWebApiDependencyRegistrar : AbstractWebApiDependencyRegistrar
    {
        public UserWebApiDependencyRegistrar(IServiceCollection services) : base(services)
        {
        }
        public UserWebApiDependencyRegistrar(IApplicationBuilder app) : base(app)
        {
        }


        public override void AddAdnc()
        {
            AddWebApiDefault<BearerAuthenticationLocalProcessor, PermissionLocalHandler>();
            AddHealthChecks(true, false, true, false);
        }

        public override void UseAdnc()
        {
            UseWebApiDefault();
        }

    }

}
