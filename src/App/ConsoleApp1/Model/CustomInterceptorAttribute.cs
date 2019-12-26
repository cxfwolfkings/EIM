using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    /// <summary>
    /// 如果直接使用Polly，那么就会造成业务代码中混杂大量的业务无关代码。
    /// 我们使用 AOP 的方式封装一个简单的框架，模仿 Spring cloud 中的 Hystrix。
    /// 
    /// 需要先引入一个支持 .Net Core 的 AOP，目前发现的最好的 .Net Core 下的 AOP 框架是 AspectCore（国产，动态织入），
    /// 其他要不就是不支持 .Net Core，要不就是不支持对异步方法进行拦截。MVC Filter
    /// GitHub：https://github.com/dotnetcore/AspectCore-Framework
    /// </summary>
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 每个被拦截的方法中执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("Before service call");
                await next(context);
            }
            catch (Exception)
            {
                Console.WriteLine("Service threw an exception!");
                throw;
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }

    }
}
