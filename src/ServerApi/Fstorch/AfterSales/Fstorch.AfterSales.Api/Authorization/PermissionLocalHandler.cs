
namespace Fstorch.AfterSales.Api.Authorization
{
    public sealed class PermissionLocalHandler : AbstractPermissionHandler
    {

        private readonly IOrganizationService _organizationService;

        public PermissionLocalHandler(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        protected override async Task<bool> CheckUserPermissions(long userId, IEnumerable<string> requestPermissions, string userBelongsRoleIds)
        {
            return true;
        }
    }
}
