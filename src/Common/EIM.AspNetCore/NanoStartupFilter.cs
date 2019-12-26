using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace EIM.AspNetCore
{
    public class NanoStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Use(async (context, _next) =>
                {
                    context.Response.Headers.Add("Server", "NanoFabric Server");
                    await _next();
                });
                next(app);
            };
        }
    }
}
