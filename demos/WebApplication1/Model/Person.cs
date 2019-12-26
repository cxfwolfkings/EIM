using System;
using System.Threading.Tasks;
using WebApplication1.Extend;

namespace WebApplication1.Model
{
    public class Person // 需要public类
    {
        [HystrixCommand(nameof(HelloFallBackAsync))]
        public virtual async Task<string> HelloAsync(string name) // 需要是虚方法
        {
            Console.WriteLine("hello" + name);
            String s = null;
            s.ToString();
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
            String s = null;
            //  s.ToArray();
            return i + j;
        }
        public int AddFall(int i, int j)
        {
            return 0;
        }
    }
}
