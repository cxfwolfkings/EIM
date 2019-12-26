using Consul;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    /// <summary>
    /// 解析RestTemplate代码。主要作用:
    /// 
    /// 1) 根据url到Consul中根据服务的名字解析获取一个服务实例,把路径转换为实际连接的服务器;负载均衡,这里用的是简单的随机负载均衡,这样服务的消费者就不用自己指定要访问那个服务提供,者了,解耦、负载均衡。
    /// 2) 负载均衡还可以根据权重随机(不同服务器的性能不一样, 这样注册服务的时候通过Tags来区,"分),还可以根据消费者IP地址来选择服务实例(涉及到一致性Hash的优化)等。
    /// 3) RestTemplate还负责把响应的ison反序列化返回结果。服务的注册者、消费者都是网站内部服务器之间的事情,对于终端用户是不涉及这些的。
    /// 
    /// 终端用户是不访问consul的。对终端用户来讲是对的Web服务器, Web服务器是服务的消费者。
    /// </summary>
    public class RestTemplate
    {
        private string consulServerUrl;

        public RestTemplate(string consulServerUrl)
        {
            this.consulServerUrl = consulServerUrl;
        }

        /// <summary>
        /// 获取服务的一个IP地址
        /// </summary>
        /// <param name="serviceName">consul服务IP</param>
        /// <returns></returns>
        private async Task<string> ResolveRootUrlAsync(string serviceName)
        {
            using (var consulClient = new ConsulClient(c => c.Address = new Uri(consulServerUrl)))
            {
                var services = (await consulClient.Agent.Services()).Response;
                var agentServices = services.Where(s => s.Value.Service.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase)).Select(s => s.Value);
                // TODO:注入负载均衡策略
                var agentService = agentServices.ElementAt(Environment.TickCount % agentServices.Count());
                // 根据当前TickCount对服务器个数取模，“随机”取一个机器出来，避免“轮询”的负载均衡策略需要计数加锁问题
                return agentService.Address + ":" + agentService.Port;
            }
        }

        /// <summary>
        /// 把http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
        /// </summary>
        private async Task<string> ResolveUrlAsync(string url)
        {
            Uri uri = new Uri(url);
            string serviceName = uri.Host;//apiservice1
            string realRootUrl = await ResolveRootUrlAsync(serviceName);
            return uri.Scheme + "://" + realRootUrl + uri.PathAndQuery;
        }

        /// <summary>
        /// Get请求转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="requestHeaders">请求头</param>
        /// <returns></returns>
        public async Task<ResponseEntity<T>> GetForEntityAsync<T>(string url, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage requestMsg = new HttpRequestMessage();
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = HttpMethod.Get;
                // http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));

                var result = await httpClient.SendAsync(requestMsg);
                ResponseEntity<T> responseEntity = new ResponseEntity<T>();
                responseEntity.StatusCode = result.StatusCode;
                string bodyStr = await result.Content.ReadAsStringAsync();
                responseEntity.Body = JsonConvert.DeserializeObject<T>(bodyStr);
                responseEntity.Headers = responseEntity.Headers;
                return responseEntity;
            }
        }
    }
}
