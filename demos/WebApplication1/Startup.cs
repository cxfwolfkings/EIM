using AspectCore.Extensions.DependencyInjection;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Extend;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // 在asp.net core项目中，可以借助于asp.net core的依赖注入，简化代理类对象的注入，
            // 不用再自己调用 ProxyGeneratorBuilder 进行代理类对象的注入了。
            //services.AddScoped<Person>();
            RegisterServices(this.GetType().Assembly, services);
            // Install-Package AspectCore.Extensions.DependencyInjection
            // 让aspectcore接管注入
            return services.BuildDynamicProxyProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 部署到不同服务器的时候不能写成127.0.0.1或者0.0.0.0，因为这是让服务消费者调用的地址
            string ip = Configuration["ip"];
            int port = int.Parse(Configuration["port"]);
            // 向consul注册服务
            ConsulClient client = new ConsulClient(ConfigurationOverview);
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                // 服务编号，不能重复，用Guid最简单
                ID = "apiservice1" + Guid.NewGuid(),
                // 服务的名字
                Name = "apiservice1",
                // 我的ip地址（可以被其他应用访问的地址，本地测试可以用127.0.0.1，机房环境中一定要写自己的内网ip地址）
                Address = ip,
                // 我的端口
                Port = port,
                Check = new AgentServiceCheck()
                {
                    // 服务停止多久后反注册
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 健康检查时间间隔，或者称为心跳间隔
                    Interval = TimeSpan.FromSeconds(10),
                    // 健康检查地址
                    HTTP = $"http://{ip}:{port}/api/health",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
        }

        private static void ConfigurationOverview(ConsulClientConfiguration obj)
        {
            obj.Address = new Uri("http://127.0.0.1:8500");
            obj.Datacenter = "dc1";
        }

        /// <summary>
        /// 根据特性批量注入
        /// 
        /// 通过反射扫描所有Service类，只要类中有标记了CustomInterceptorAttribute的方法都算作服务实现类。
        /// 为了避免一下子扫描所有类，所以RegisterServices还是手动指定从哪个程序集中加载。
        /// </summary>
        private static void RegisterServices(Assembly assembly, IServiceCollection services)
        {
            // 遍历程序集中的所有public类型
            foreach (Type type in assembly.GetExportedTypes())
            {
                // 判断类中是否有标注了CustomInterceptorAttribute的方法
                bool hasHystrixCommandAttr = type.GetMethods().Any(m => m.GetCustomAttribute(typeof(HystrixCommandAttribute)) != null);
                if (hasHystrixCommandAttr)
                {
                    services.AddSingleton(type);
                }
            }
        }
    }
}
