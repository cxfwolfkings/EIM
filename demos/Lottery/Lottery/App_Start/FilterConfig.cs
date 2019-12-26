using Lottery.Filter;
using System.Web.Mvc;

namespace Lottery
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomErrorFilter());
        }
    }
}
