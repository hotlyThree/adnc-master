


using Adnc.Shared.Rpc.Http.Services;

namespace Fstorch.AfterSales.Application
{
    public class DependencyRegistrar : AbstractApplicationDependencyRegistrar
    {

        private readonly Assembly _assembly;

        public DependencyRegistrar(IServiceCollection services) : base(services)
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public override Assembly ApplicationLayerAssembly => _assembly;

        public override Assembly ContractsLayerAssembly => typeof(IOrganizationService).Assembly;

        public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityInfo).Assembly;

        public override void AddAdnc()
        {
            AddApplicaitonDefault();
            var restPolicies = PollyStrategyEnable ? this.GenerateDefaultRefitPolicies() : new();

            AddRestClient<IAuthRestClient>(ServiceAddressConsts.AdncFstorchSystemService, restPolicies);
            AddRestClient<ISystemRestClient>(ServiceAddressConsts.AdncFstorchSystemService, restPolicies);
            AddRestClient<IStaffRestClient>(ServiceAddressConsts.AdncFstorchSystemService, restPolicies);
        }
    }
}
