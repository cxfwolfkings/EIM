using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace MVCApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // 关闭了 JWT Claim类型映射，以允许常用的Claim（例如'sub'和'idp'）
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // AddAuthentication将身份认证服务添加到 DI
            services.AddAuthentication(options =>
            {
                // 使用 cookie 来本地登录用户（通过"Cookies"作为DefaultScheme）
                options.DefaultScheme = "Cookies";
                // 将 DefaultChallengeScheme 设置为"oidc"，因为当我们需要用户登录时，我们将使用 OpenID Connect 协议。
                options.DefaultChallengeScheme = "oidc";
            })
                // 使用 AddCookie 添加可以处理 cookie 的处理程序
                .AddCookie("Cookies")
                // AddOpenIdConnect 用于配置执行 OpenID Connect 协议的处理程序
                .AddOpenIdConnect("oidc", options =>
                {
                    // Authority 表明我们信任的 IdentityServer 地址
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    // 通过 ClientId 识别这个客户端
                    options.ClientId = "mvc";
                    // SaveTokens 用于在 cookie 中保留来自 IdentityServer 的令牌
                    options.SaveTokens = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // 确保认证服务执行对每个请求的验证，加入 UseAuthentication 到 Configure 中
            // 在管道中的 MVC 之前添加认证中间件
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
/**
 * 最后一步是触发身份认证。为此，请转到 Controller 并添加 [Authorize] 特性到其中一个Action。
 * 还要修改主视图以显示用户的 Claim 以及 cookie 属性
 */
