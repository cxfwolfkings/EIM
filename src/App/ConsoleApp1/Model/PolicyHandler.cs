using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.MemoryCache;
using Polly.Timeout;
using System;
using System.Threading;

namespace ConsoleApp1.Model
{
    public class PolicyHandler
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public void PolicyDemo1()
        {
            try
            {
                ISyncPolicy policy = Policy
                    .Handle<ArgumentException>(ex => ex.Message == "年龄参数错误")
                    .Or<NullReferenceException>()
                    .Or<StackOverflowException>()
                    .Fallback(() =>
                    {
                        Console.WriteLine("出错了");
                    });
                policy.Execute(() =>
                {
                    // 这里是可能会产生问题的业务系统代码
                    Console.WriteLine("开始任务");
                    throw new ArgumentException("年龄参数错误");
                    // throw new Exception("haha");
                    // Console.WriteLine("完成任务");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"未处理异常:{ex}");
            }
        }

        /// <summary>
        /// 重试处理
        /// </summary>
        public void PolicyDemo2()
        {
            try
            {
                /**
                 * RetryForever()是一直重试直到成功
                 * Retry()是重试最多一次；
                 * Retry(n)是重试最多n次；
                 * WaitAndRetry()可以实现“如果出错等待100ms再试还不行再等150ms秒...”
                 */
                ISyncPolicy policy = Policy
                    .Handle<Exception>()
                    .RetryForever(); // 一直重试
                policy.Execute(() =>
                {
                    Console.WriteLine("开始任务");
                    if (DateTime.Now.Second % 10 != 0)
                    {
                        throw new Exception("出错");
                    }
                    Console.WriteLine("完成任务");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"未处理异常:{ex}");
            }
        }

        /// <summary>
        /// 短路保护Circuit Breaker
        /// 
        /// 出现N次连续错误，则把“熔断器”（保险丝）熔断，等待一段时间，等待这段时间内如果再Execute则直接抛出BrokenCircuitException异常。
        /// 等待时间过去之后，再执行Execute的时候如果又错了（一次就够了），那么继续熔断一段时间，否则就回复正常。
        /// 这样就避免一个服务已经不可用了，还是使劲的请求给系统造成更大压力。
        /// </summary>
        public void PolicyDemo3()
        {
            // 连续出错6次之后熔断5秒（不会再去尝试执行业务代码）。
            ISyncPolicy policy = Policy.Handle<Exception>().CircuitBreaker(6, TimeSpan.FromSeconds(5));
            while (true)
            {
                Console.WriteLine("开始Execute");
                try
                {
                    policy.Execute(() =>
                    {
                        Console.WriteLine("开始任务");
                        throw new Exception("出错");
                        Console.WriteLine("完成任务");
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("execute出错" + ex.GetType() + ":" + ex.Message);
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 策略封装
        /// </summary>
        public void PolicyDemo4()
        {
            ISyncPolicy policy1 = Policy.Handle<Exception>().Retry(1);
            ISyncPolicy policy2 = Policy.Handle<Exception>().Retry(1);
            // 执行policy3就会把policy1、policy2封装到一起执行
            ISyncPolicy policy3 = policy1.Wrap(policy2);
        }

        /// <summary>
        /// 超时处理
        /// 
        /// 下面的代码就是如果执行超过3秒钟，则直接Fallback，Execute中的代码也会被强行终止（引发TimeoutRejectedException异常）。
        /// 这个的用途：请求网络接口，避免接口长期没有响应造成系统卡死。
        /// TimeoutStrategy.Optimistic 是主动通知代码，告诉他“到期了”，由代码自己决定是不是继续执行，局限性很大，一般不用。
        /// </summary>
        public void PolicyDemo5()
        {
            ISyncPolicy policy = Policy.Handle<Exception>()
                .Fallback(() =>
                {
                    Console.WriteLine("执行出错");
                });
            // 创建一个3秒钟（注意单位）的超时策略。超时策略一般不能直接用，而是和其他封装到一起用：
            policy = policy.Wrap(Policy.Timeout(3, TimeoutStrategy.Pessimistic));

            policy.Execute(() =>
            {
                Console.WriteLine("开始任务");
                Thread.Sleep(5000);
                Console.WriteLine("完成任务");
            });
        }

        /// <summary>
        /// 下面的代码，如果发生超时，重试最多3次（也就是说一共执行4次哦）。
        /// </summary>
        public void PolicyDemo6()
        {
            ISyncPolicy policy = Policy.Handle<TimeoutRejectedException>().Retry(1);
            policy = policy.Wrap(Policy.Timeout(3, TimeoutStrategy.Pessimistic));
            policy.Execute(() =>
            {
                Console.WriteLine("开始任务");
                Thread.Sleep(5000);
                Console.WriteLine("完成任务");
            });
        }

        /// <summary>
        /// 缓存
        /// 
        /// 缓存的意思就是N秒内只调用一次方法，其他的调用都返回缓存的数据。
        /// 目前只支持Polly 5.9.0，不支持最新版
        /// 功能局限性也大，简单讲一下，后续先不用这个实现缓存原则：别人的好用我就拿来用，不好用我就自己造。
        /// 命令空间都写到代码中，因为有容易引起混淆的同名类。
        /// </summary>
        public void PolicyDemo7()
        {
            // Install-Package Microsoft.Extensions.Caching.Memory
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            // Install-Package Polly.Caching.MemoryCache
            MemoryCacheProvider memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            CachePolicy policy = Policy.Cache(memoryCacheProvider, TimeSpan.FromSeconds(5));
            Random rand = new Random();
            while (true)
            {
                int i = rand.Next(5);
                Console.WriteLine("产生" + i);
                var context = new Context("doublecache" + i);
                int result = policy.Execute(ctx =>
                {
                    Console.WriteLine("Execute计算" + i);
                    return i * 2;
                }, context);
                Console.WriteLine("计算结果：" + result);
                Thread.Sleep(500);
            }
        }
    }
}
