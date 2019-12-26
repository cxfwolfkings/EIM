using AspectCore.DynamicProxy;
using ConsoleApp1.Model;
using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static void QueryConsul1()
        {
            using (ConsulClient consulClient = new ConsulClient(
                c => c.Address = new Uri("http://127.0.0.1:8500")))
            {
                // consulClient.Agent.Services()获取consul中注册的所有的服务
                Dictionary<string, AgentService> services = consulClient.Agent.Services().Result.Response;
                foreach (KeyValuePair<string, AgentService> kv in services)
                {
                    Console.WriteLine($"key={kv.Key},{kv.Value.Address},{kv.Value.ID},{kv.Value.Service},{kv.Value.Port}");
                }
                // 获取所有服务名字是"apiservice1"所有的服务
                var agentServices = services.Where(s => s.Value.Service.Equals("apiservice1", StringComparison.CurrentCultureIgnoreCase))
                   .Select(s => s.Value);
                // 根据当前TickCount对服务器个数取模，“随机”取一个机器出来，避免“轮询”的负载均衡策略需要计数加锁问题
                var agentService = agentServices.ElementAt(Environment.TickCount % agentServices.Count());
                Console.WriteLine($"{agentService.Address},{agentService.ID},{agentService.Service},{agentService.Port}");
            }
        }

        static void DoService1()
        {
            RestTemplate rest = new RestTemplate("http://127.0.0.1:8500");
            // RestTemplate把服务的解析和发请求以及响应反序列化帮我们完成
            ResponseEntity<string[]> resp = rest.GetForEntityAsync<string[]>("http://apiservice1/api/values").Result;
            Console.WriteLine(resp.StatusCode);
            Console.WriteLine(string.Join(",", resp.Body));
        }

        /// <summary>
        /// 通过 AspectCore 创建代理对象
        /// </summary>
        static void Interceptor1()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                // 注意 p 指向的对象是 AspectCore 生成的 Person 的动态子类的对象
                // 直接 new Person 是无法被拦截的
                Person p = proxyGenerator.CreateClassProxy<Person>();
                p.Say("Hello World");
                Console.WriteLine(p.GetType());
                Console.WriteLine(p.GetType().BaseType);
            }
        }

        static void Interceptor2()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Animal cat = proxyGenerator.CreateClassProxy<Animal>();
                Console.WriteLine(cat.HelloAsync("Hello World").Result);
                Console.WriteLine(cat.Add(1, 2));
            }
        }

        static void Interceptor3()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Animal1 p = proxyGenerator.CreateClassProxy<Animal1>();
                Console.WriteLine(p.HelloAsync("Hello World").Result);
                Console.WriteLine(p.Add(1, 2));
                while (true)
                {
                    Console.WriteLine(p.HelloAsync("Hello World").Result);
                    Thread.Sleep(100);
                }
            }
        }
    }
}
