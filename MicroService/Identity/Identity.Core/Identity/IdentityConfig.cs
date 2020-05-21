using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Service.Identity
{
    public class IdentityConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 //密码模式
                 new Client
                 {
                    ClientId = "client",
                    // 没有交互性用户，使用 clientid/secret 实现认证。
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword ,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600 * 2400, //2400小时
                    SlidingRefreshTokenLifetime = 1296000, //15天
                    // 用于认证的密码
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes = { "api" }
                 },
            };
        }
    }
}
