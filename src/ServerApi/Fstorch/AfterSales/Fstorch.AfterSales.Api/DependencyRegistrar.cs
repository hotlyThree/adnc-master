namespace Fstorch.AfterSales.Api
{
    public class AfterSalesWebApiDependencyRegistrar : AbstractWebApiDependencyRegistrar
    {
        public AfterSalesWebApiDependencyRegistrar(IApplicationBuilder app) : base(app)
        {
        }

        public AfterSalesWebApiDependencyRegistrar(IServiceCollection services) : base(services)
        {
        }

        public override void AddAdnc()
        {
            //AddWebApiDefault<BearerAuthenticationLocalProcessor, PermissionLocalHandler>();
            //AddWebApiNoAuthDefault();
            AddWebApiDefault();
            AddHealthChecks(true, false, true, false);
        }

        public override void UseAdnc()
        {
            UseWebApiDefault();
        }
    }
}
