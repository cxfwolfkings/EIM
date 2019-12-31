using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                // discover endpoints from metadata
                var client = new HttpClient();

                // 为 "Client" 项目添加 Nuget 包：IdentityModel
                // IdentityModel 包括用于发现 IdentityServer 各个终结点（EndPoint）的客户端库。
                // 这样您只需要知道 IdentityServer 的地址 - 可以从元数据中读取实际的各个终结点地址：
                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }

                // request token
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "ro.client",
                    ClientSecret = "secret",

                    UserName = "alice",
                    Password = "password",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
            });
            Console.ReadKey();
        }
    }
}
/**
 * 当您将令牌发送到身份API终结点时，您会注意到与客户端模式相比有一个小但重要的区别。访问令牌现在将包含唯一标识用户的sub claim。 
 * 通过在调用API之后检查内容变量可以看到这个"sub"，并且控制器应用程序也会在屏幕上显示该claim。
 * sub claim的存在（或不存在）允许API区分代表客户端的调用和代表用户的调用。
 */
