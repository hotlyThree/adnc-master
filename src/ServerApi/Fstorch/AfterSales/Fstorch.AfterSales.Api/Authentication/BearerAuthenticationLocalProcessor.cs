namespace Fstorch.AfterSales.Api.Authentication
{
    public class BearerAuthenticationLocalProcessor : AbstractAuthenticationProcessor
    {
        private readonly IOrganizationService _organizationService;

        public BearerAuthenticationLocalProcessor(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        protected override async Task<(string? ValidationVersion, int Status)> GetValidatedInfoAsync(long userId)
        {
            return ("bdc517f2e534479eb1852d69e6054346", 1);
        }
    }
}
