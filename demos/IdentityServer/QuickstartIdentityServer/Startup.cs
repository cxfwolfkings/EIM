using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                // 将测试用户注册到 IdentityServer
                .AddTestUsers(Config.GetUsers());
            // AddTestUsers 方法帮我们做了以下几件事：
            // 1. 为资源所有者密码授权添加支持
            // 2. 添加对用户相关服务的支持，这服务通常为登录 UI 所使用（我们将在下一个快速入门中用到登录 UI）
            // 3. 为基于测试用户的身份信息服务添加支持（你将在下一个快速入门中学习更多与之相关的东西）
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            // uncomment if you want to support static files
            //app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you wan to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}
/**
 * 运行此项目，打开浏览器访问 http://localhost:5000/.well-known/openid-configuration 你将会看到 IdentityServer 的各种元数据信息。
 * 首次启动时，IdentityServer将为您创建一个开发人员签名密钥，它是一个名为tempkey.rsa的文件。 
 * 您不必将该文件检入源代码管理中，如果该文件不存在，将重新创建该文件。
 */
