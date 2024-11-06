
namespace Adnc.Fstorch.User.Application
{

    public sealed class UserApplicationDependencyRegistrar : AbstractApplicationDependencyRegistrar
    {
        private readonly Assembly _assembly;
        public UserApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public override Assembly ApplicationLayerAssembly => _assembly;

        public override Assembly ContractsLayerAssembly => typeof(IUserAppService).Assembly;

        public override Assembly RepositoryOrDomainLayerAssembly => typeof(EntityInfo).Assembly;

        public override void AddAdnc()
        {
            AddApplicaitonDefault();
        }
    }
}
