using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APIService1
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 最后一步是将身份验证服务添加到DI和身份验证中间件到管道。这些将：
            // 验证传入令牌以确保它来自受信任的颁发者
            // 验证令牌是否有效用于此API（也称为 audience）
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            // AddAuthentication 将身份认证服务添加到DI，并将 "Bearer" 配置为默认方案。 
            // AddJwtBearer 将 JWT 认证处理程序添加到 DI 中以供身份认证服务使用
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // UseAuthentication 将身份认证中间件添加到管道中，因此将在每次调用API时自动执行身份验证
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

/**
 * 如果在浏览器访问（http:// localhost:5001/identity），你会得到HTTP 401的结果。这意味着您的API需要凭据。
 * 就是这样，API现在受 IdentityServer 保护。
 */
