
namespace Adnc.Fstorch.File.Api
{
    public sealed class FileWebApiDependencyRegistrar : AbstractWebApiDependencyRegistrar
    {
        public FileWebApiDependencyRegistrar(IServiceCollection services) : base(services)
        {

        }

        public FileWebApiDependencyRegistrar(IApplicationBuilder app) : base(app)
        {

        }

        public override void AddAdnc()
        {
            AddWebApiDefault();
            AddHealthChecks(true, false,true, false);
        }

        public override void UseAdnc()
        {
            UseWebApiDefault();
        }



    }
}
