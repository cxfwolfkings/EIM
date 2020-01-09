using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Demo());
            Console.ReadKey();
        }

        static async Task Demo()
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

            // 接下来，您可以使用从 IdentityServer 元数据获取到的Token终结点请求令牌：
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // 要将Token发送到API，通常使用HTTP Authorization标头。 这是使用SetBearerToken扩展方法完成的：
            // call api
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                // 默认情况下，Token将包含有关 Scope，生命周期（nbf和exp），客户端ID（client_id）和颁发者名称（iss）的身份信息单元（Claim）。
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
