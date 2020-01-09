using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace QuickstartIdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 添加对标准 openid（subject id）和profile （名字，姓氏等）Scope的支持
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // 对于这种情况，客户端将不具有交互式（人机交互）用户，并将使用IdentityServer的客户端模式进行身份验证
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },

                // 为资源所有者密码授权添加一个客户端定义
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                // 基于 OpenID Connect 的客户端与我们目前添加的 OAuth 2.0 客户端非常相似。
                // 但由于 OIDC 中的流程始终是交互式的，因此我们需要在配置中添加一些重定向URL。
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // 登录成功回调处理地址，处理回调返回的数据
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    // 可以使用客户端配置上RequireConsent的属性关闭同意授权页面
                    RequireConsent = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        /// <summary>
        /// 就像API资源（也称为 Scope）、客户端一样，用户也有一个基于内存存储（In-Memory）的实现。
        /// TestUser 类代表测试用户及其身份信息单元（Claim）。让我们通过在 config 类中添加以下代码来创建几个用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }
    }
}
