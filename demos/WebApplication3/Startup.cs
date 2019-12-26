using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string ip = "localhost";
            int port = 4001;

            // 向consul注册服务
            ConsulClient client = new ConsulClient(config => config.Address = new Uri("http://127.0.0.1:8500"));
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "duanxin" + Guid.NewGuid(), // 服务编号，不能重复，用Guid最简单
                Name = "duanxin", // 服务的名字
                Address = ip, // 我的ip地址（可以被其他应用访问的地址，本地测试可以用127.0.0.1，机房环境中一定要写自己的内网ip地址）
                Port = port, // 我的端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务停止多久后反注册
                    Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔，或者称为心跳间隔
                    HTTP = $"http://{ip}:{port}/api/health", // 健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
