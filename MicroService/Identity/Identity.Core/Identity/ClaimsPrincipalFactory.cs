using Identity.Core.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Service.Identity
{
    /// <summary>
    /// 重写GenerateClaimsAsync方法， 解决sub claims is missing的问题
    /// </summary>
    public sealed class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public ClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, roleManager, optionsAccessor)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);

            if (!identity.HasClaim(x => x.Type == JwtClaimTypes.Subject))
            {
                var sub = user.Id;
                identity.AddClaim(new Claim(JwtClaimTypes.Subject, sub));
            }
            return identity;
        }

    }
}
