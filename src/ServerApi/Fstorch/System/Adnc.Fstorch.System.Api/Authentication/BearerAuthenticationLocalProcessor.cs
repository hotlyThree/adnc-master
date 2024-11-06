using Adnc.Shared.WebApi.Authentication;

namespace Adnc.Fstorch.System.Api.Authentication
{
    public class BearerAuthenticationLocalProcessor : AbstractAuthenticationProcessor
    {
        protected override async Task<(string? ValidationVersion, int Status)> GetValidatedInfoAsync(long userId)
        {
            return ("N445456", 1);
        }
    }

}
