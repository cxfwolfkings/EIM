using AspectCore.DynamicProxy;
using Polly;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    /// <summary>
    /// 细化框架：
    /// 重试：MaxRetryTimes表示最多重试几次，如果为0则不重试，RetrvIntervalMilliseconds表示重试间隔的豪秒数
    /// 熔断：EnableCircuitBreaker是否启用熔断，ExceptionsAllowedBeforeBreaking表示出现错误，几次后熔断，Milliseconds0fBreak表示熔断多长时间（毫秒）
    /// 超时：TimeOutMilliseconds执行超过多少毫秒则认为超时（0表示不检测超时）
    /// 缓存：缓存多少豪秒（0表示不缓存），用“类名+方法名+所有参数ToString拼接”做缓存Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HystrixCommand1Attribute : AbstractInterceptorAttribute
    {
        #region 属性
        /// <summary>
        /// 最多重试几次，如果为0则不重试
        /// </summary>
        public int MaxRetryTimes { get; set; } = 0;

        /// <summary>
        /// 重试间隔的毫秒数
        /// </summary>
        public int RetryIntervalMilliseconds { get; set; } = 100;

        /// <summary>
        /// 是否启用熔断
        /// </summary>
        public bool EnableCircuitBreaker { get; set; } = false;

        /// <summary>
        /// 熔断前出现允许错误几次
        /// </summary>
        public int ExceptionAllowedBeforeBreaking { get; set; } = 3;

        /// <summary>
        /// 熔断多长时间（毫秒 ）
        /// </summary>
        public int MillisecondsOfBreak { get; set; } = 1000;

        /// <summary>
        /// 执行超过多少毫秒则认为超时（0表示不检测超时）
        /// </summary>
        public int TimeOutMilliseconds { get; set; } = 0;

        /// <summary>
        /// 缓存多少毫秒（0表示不缓存），用“类名+方法名+所有参数ToString拼接”做缓存Key
        /// </summary>
        public int CacheTTLMilliseconds { get; set; } = 0;

        private Policy policy;

        //缓存
        private static readonly Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions());

        /// <summary>
        /// 降级方法名
        /// </summary>
        public string FallBackMethod { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 熔断框架
        /// </summary>
        /// <param name="fallBackMethod">降级方法名</param>
        public HystrixCommand1Attribute(string fallBackMethod)
        {
            this.FallBackMethod = fallBackMethod;
        }
        #endregion

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            // 一个 HystrixCommand 中保持一个 policy 对象即可
            // 其实主要是 CircuitBreaker 要求对于同一段代码要共享一个 policy 对象
            // 根据反射原理，同一个方法就对应一个 HystrixCommandAttribute，无论几次调用，
            // 而不同方法对应不同的 HystrixCommandAttribute 对象，天然的一个 policy 对象共享
            // 因为同一个方法共享一个 policy，因此这个 CircuitBreaker 是针对所有请求的。
            // Attribute 也不会在运行时再去改变属性的值，共享同一个 policy 对象也没问题
            lock (this)
            {
                if (policy == null)
                {
                    policy = Policy.Handle<Exception>().FallbackAsync(
                        async (ctx, t) =>
                        {
                            AspectContext aspectContext = (AspectContext)ctx["aspectContext"];
                            var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                            object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                            // 不能如下这样，因为这是闭包相关，如果这样写第二次调用 Invoke 的时候 context 指向的
                            // 还是第一次的对象，所以要通过 Polly 的上下文来传递 AspectContext
                            //context.ReturnValue = fallBackResult;
                            aspectContext.ReturnValue = fallBackResult;
                        },
                        async (ex, t) => { });

                    if (MaxRetryTimes > 0) // 重试
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().WaitAndRetryAsync(MaxRetryTimes, i => TimeSpan.FromMilliseconds(RetryIntervalMilliseconds)));
                    }

                    if (EnableCircuitBreaker) // 熔断
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(ExceptionAllowedBeforeBreaking, TimeSpan.FromMilliseconds(MillisecondsOfBreak)));
                    }

                    if (TimeOutMilliseconds > 0) // 超时
                    {
                        policy = policy.WrapAsync(Policy.TimeoutAsync(() => TimeSpan.FromMilliseconds(TimeOutMilliseconds), Polly.Timeout.TimeoutStrategy.Pessimistic));
                    }
                }
            }

            // 把本地调用的 AspectContext 传递给 Polly，主要给 FallBackMethod 中使用，避免闭包的坑
            Context pollyCtx = new Context();
            pollyCtx["aspectContext"] = context;

            if (CacheTTLMilliseconds > 0)
            {
                // 用类名+方法名+参数的下划线连接起来作为缓存key
                string cacheKey = "HystrixMethodCacheManager_Key_" + context.ServiceMethod.DeclaringType + "." + context.ServiceMethod + string.Join("_", context.Parameters);

                // 尝试去缓存中获取。如果找到了，则直接用缓存中的值做返回值
                if (memoryCache.TryGetValue(cacheKey, out var cacheValue))
                {
                    context.ReturnValue = cacheValue;
                }
                else
                {
                    // 如果缓存中没有，则执行实际被拦截的方法
                    await policy.ExecuteAsync(ctx => next(context), pollyCtx);
                    // 存入缓存中
                    using (var cacheEntry = memoryCache.CreateEntry(cacheKey))
                    {
                        cacheEntry.Value = context.ReturnValue;
                        cacheEntry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMilliseconds(CacheTTLMilliseconds);
                    }
                }
            }
            else // 如果没有启用缓存，就直接执行业务方法
            {
                await policy.ExecuteAsync(ctx => next(context), pollyCtx);
            }
        }
    }
}
