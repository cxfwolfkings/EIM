using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    /// <summary>
    /// 目标：在执行 HelloAsync 失败的时候自动执行 HelloFallBackAsync，达到熔断降级
    /// </summary>
    public class Animal
    {

        [HystrixCommand(nameof(HelloFallBackAsync))]
        public virtual async Task<string> HelloAsync(string name) // 需要是虚方法
        {
            Console.WriteLine("hello" + name);

            //抛错
            String s = null;
            //s.ToString();

            return "ok";
        }

        public async Task<string> HelloFallBackAsync(string name)
        {
            Console.WriteLine("执行失败" + name);
            return "fail";
        }

        [HystrixCommand(nameof(AddFall))]
        public virtual int Add(int i, int j)
        {
            //抛错
            String s = null;
            //s.ToString();

            return i + j;
        }

        public int AddFall(int i, int j)
        {
            return 0;
        }

    }
}
