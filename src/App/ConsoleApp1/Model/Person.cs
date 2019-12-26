using System;

namespace ConsoleApp1.Model
{
    /// <summary>
    /// 编写需要被代理拦截的类 Person.cs，在要被拦截的方法上标注CustomInterceptorAttribute。
    /// 类需要是public类，方法需要是虚方法，支持异步方法，因为动态代理是动态生成被代理的类的动态子类实现的。
    /// </summary>
    public class Person
    {
        [CustomInterceptor]
        public virtual void Say(string msg)
        {
            Console.WriteLine("service calling..." + msg);
        }
    }
}
