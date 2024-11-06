using Adnc.Shared.Application.Registrar;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Adnc.Fstorch.System.Repository;
using Adnc.Fstorch.System.Application.Service;

namespace Adnc.Fstorch.System.Application
{

    public sealed class UserApplicationDependencyRegistrar : AbstractApplicationDependencyRegistrar
    {
        private readonly Assembly _assembly;
        public UserApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public override Assembly ApplicationLayerAssembly => _assembly;

        public override Assembly ContractsLayerAssembly => typeof(IGroupInfoService).Assembly;

        public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityInfo).Assembly;

        public override void AddAdnc()
        {
            AddApplicaitonDefault();
        }
    }
}
