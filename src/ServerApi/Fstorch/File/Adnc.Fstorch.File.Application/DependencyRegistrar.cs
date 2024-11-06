



namespace Adnc.Fstorch.File.Application
{
    public sealed class FileApplicationDependencyRegistrar : AbstractApplicationDependencyRegistrar
    {
        private readonly Assembly _assembly;
        public FileApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public override Assembly ApplicationLayerAssembly => _assembly;

        public override Assembly ContractsLayerAssembly => typeof(ICardFileAppService).Assembly;

        public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityInfo).Assembly;

        public override void AddAdnc()
        {
            //register base services
            AddApplicaitonDefault();
            //register rpc-http services
            var restPolicies = PollyStrategyEnable ? this.GenerateDefaultRefitPolicies() : new();
            AddRestClient<IAuthRestClient>(ServiceAddressConsts.AdncDemoAuthService, restPolicies);
            AddRestClient<IUserRestClient>(ServiceAddressConsts.AdncDemoUserService, restPolicies);
            
            var gprcPolicies = this.GenerateDefaultGrpcPolicies();
            AddGrpcClient<MsgGrpc.MsgGrpcClient>(ServiceAddressConsts.AdncDemoUserService, gprcPolicies);
        }
    }
}
