using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lottery
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterLog4Net();
        }

        /// <summary>
        /// 注册log4net
        /// </summary>
        private void RegisterLog4Net()
        {
            FileInfo file = new FileInfo(Server.MapPath("~\\log4net.config"));
            log4net.Config.XmlConfigurator.Configure(file);
        }
    }
}
