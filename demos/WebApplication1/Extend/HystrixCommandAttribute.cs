using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Extend
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HystrixCommandAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 降级方法名
        /// </summary>
        public string FallBackMethod { get; set; }

        /// <summary>
        /// 熔断框架
        /// </summary>
        /// <param name="fallBackMethod">降级方法名</param>
        public HystrixCommandAttribute(string fallBackMethod)
        {
            this.FallBackMethod = fallBackMethod;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);//执行被拦截的方法
            }
            catch
            {
                var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                context.ReturnValue = fallBackResult;
                await Task.FromResult(0);
            }
        }
    }
}
